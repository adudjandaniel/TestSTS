using System;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TestSTS.IdentityServer;
using TestSTS.Models.Account;
using Microsoft.AspNetCore.Http;

namespace TestSTS.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events
        )
        {
            _users = new TestUserStore(TestUsers.Users);

            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginInputModel inputModel)
        {
            var context = await _interaction.GetAuthorizationContextAsync(inputModel.ReturnUrl);

            if (ModelState.IsValid)
            {
                if (_users.ValidateCredentials(inputModel.Username, inputModel.Password))
                {
                    var user = _users.FindByUsername(inputModel.Username);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.SubjectId, 
                        user.Username, clientId: context?.Client.ClientId));

                    var issuer = new IdentityServerUser(user.SubjectId)
                    {
                        DisplayName = user.Username
                    };

                    await HttpContext.SignInAsync(issuer, null);

                    return new JsonResult(new { RedirectUrl = inputModel.ReturnUrl, ok = true});
                }
            }
            else
            {
                throw new Exception("Invalid model");
            }

            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var context = await _interaction.GetLogoutContextAsync(logoutId);
            bool showSignoutPrompt = true;

            if (context?.ShowSignoutPrompt == false)
            {
                showSignoutPrompt = false;
            }

            if (User?.Identity.IsAuthenticated == true)
            {
                await HttpContext.SignOutAsync();
            }

            return Ok(new 
            {
                showSignoutPrompt,
                ClientName = string.IsNullOrEmpty(context?.ClientName) ? context?.ClientId : context?.ClientName,
                context?.PostLogoutRedirectUri,
                context?.SignOutIFrameUrl,
                logoutId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Client([FromBody] LoginInputModel inputModel)
        {
            var context = await _interaction.GetAuthorizationContextAsync(inputModel.ReturnUrl);

            if (context == null)
            {
                return Ok($"Context is null");
            }

            var client = await _clientStore.FindClientByIdAsync(context.Client.ClientId);

            if (client == null)
            {
                return Ok("Client is null");
            }

            return new JsonResult(new { ClientName = client.ClientName});
        }
    }
}