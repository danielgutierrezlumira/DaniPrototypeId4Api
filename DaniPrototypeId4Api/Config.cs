using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace DaniPrototypeId4Api
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource(@"api1", @"My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = @"client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret(@"secret".Sha512())
                    },
                    AllowedScopes =
                    {
                        @"api1"
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = @"1",
                    Username = @"aaron",
                    Password = @"INRstar_5"
                },
                new TestUser
                {
                    SubjectId = @"2",
                    Username = @"hannah",
                    Password = @"Banana_5"
                }
            };
        }
    }
}
