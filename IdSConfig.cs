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
                },
                new Client
                {
                    ClientId = "PostmanTest",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = 
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "ApiResource1.scope1" }
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
                new ApiResource
                {
                    Name = "ApiResource1",
                    Scopes = { "ApiResource1.scope1" }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("ApiResource1.scope1")
            };
        }
    }
}