using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReenWise.ExternalApi.Models;
using RestSharp;

namespace ReenWise.ExternalApi.Authentication
{
    public class AuthenticateToken
    {
        private static int authenticateFailureCount = 0;
        public static AccessToken GetToken()
        {
            try
            {
                // TODO: Find a secure way to store ClientId and ClientSecret. External encrypted file?
                // TODO: Add timer so that token is collected in intervals. Token lasts for 1 hour.

                string clientId = "placeholder";
                string clientSecret = "placeholder";

                var client = new RestClient("https://identity.abax.cloud/connect/token");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded",
                    $"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}&audience=https://identity.abax.cloud/connect/token",
                    ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var accessToken = JsonConvert.DeserializeObject<AccessToken>(response.Content);

                    return accessToken;
                }
                if (!response.IsSuccessful && authenticateFailureCount <= 3)
                {
                    authenticateFailureCount++;
                    GetToken();
                    if (response.IsSuccessful)
                    {
                        var accessToken = JsonConvert.DeserializeObject<AccessToken>(response.Content);

                        return accessToken;
                    }
                }
                else
                {
                    throw new Exception("Unable to collect access token");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
