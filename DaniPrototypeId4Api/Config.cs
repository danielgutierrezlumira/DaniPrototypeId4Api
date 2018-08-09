using System.Collections.Generic;
using IdentityServer4.Models;

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
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
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
    }
}
