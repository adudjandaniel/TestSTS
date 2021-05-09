using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace TestSTS.IdentityServer
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1234",
                Username = "danny",
                Password = "9e3MUUU=%H*$MC,",
                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "Daniel Adu-Djan"),
                    new Claim(JwtClaimTypes.GivenName, "Daniel"),
                    new Claim(JwtClaimTypes.FamilyName, "Adu-Djan"),
                    new Claim(JwtClaimTypes.Email, "daniel@luzcode.com")
                }
            }
        };
    }
}