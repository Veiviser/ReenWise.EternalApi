using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReenWise.ExternalApi.Models;
using RestSharp;

namespace ReenWise.ExternalApi
{
    public class Post
    {
        public static void PostData(List<DataTransferObject> dataCollection)
        {
            try
            {

                foreach (var data in dataCollection)
                {
                    string destination = data.origin;
                    string content = data.data;

                    // TODO: Add the reen url to the rest client destination.
                    var clientDestination = new RestClient("Placeholder");

                    // TODO: Add authentication when client has implemented it.
                    //var authentication = new JwtAuthenticator("placeholder");
                    //clientDestination.Authenticator = authentication;

                    var request = new RestRequest(Method.POST);
                    var response = clientDestination.Execute(request);


                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
