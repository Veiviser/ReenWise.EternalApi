using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReenWise.ExternalApi.Authentication;
using ReenWise.ExternalApi.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace ReenWise.ExternalApi
{
    public class Get
    {
        private static int getFailureCount;
        public static List<DataTransferObject> GetData(string token)
        {
            DataTransferObject result;
            List<DataTransferObject> results = new List<DataTransferObject>();
            string[] apiCalls = new[]
            {
                "equipment",
                "vehicle"
            };

            foreach (var apiCall in apiCalls)
            {

                var clientDestination = new RestClient("https://api-test.abax.cloud/v1/" + apiCall);

                var authentication = new JwtAuthenticator(token);
                clientDestination.Authenticator = authentication;

                var request = new RestRequest(Method.GET);
                var response = clientDestination.Execute(request);

                if (response.IsSuccessful)
                {
                    response.Content = JValue.Parse(response.Content).ToString(Formatting.Indented);

                    result = new DataTransferObject();

                    result.data = response.Content;
                    result.origin = apiCall;

                    results.Add(result);
                    
                }
                else if (!response.IsSuccessful && getFailureCount <= 3)
                {
                    getFailureCount++;
                    AuthenticateToken.GetToken();
                }
            }
            return results;
        }
    }
}
