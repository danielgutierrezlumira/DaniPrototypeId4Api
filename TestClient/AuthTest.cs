using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace TestClient
{
    internal class AuthTest
    {
        public async Task StartTest()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:8080");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            using (var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret"))
            using (var client = new HttpClient())
            {
                // request token
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return;
                }

                Console.WriteLine(tokenResponse.Json);

                // call api
                client.SetBearerToken(tokenResponse.AccessToken);

                var response = await client.GetAsync("http://localhost:62453/identity");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(JToken.Parse(content));
                }
            }
        }
    }
}
