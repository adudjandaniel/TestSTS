using System.Collections.Generic;
using IdentityServer4.Models;

namespace TestSTS.IdentityServer
{
    public class IdSConfig
    {
        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "TestApp",
                    ClientName = "Recommendation System",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://localhost:5003/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:5003/authentication/logout-callback" },
                    AllowedScopes = { "openid", "email", "profile" },
                    AllowedCorsOrigins = new[]{ "https://localhost:5003" }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
            };
        }
    }
}