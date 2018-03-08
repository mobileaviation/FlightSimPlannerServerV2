using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPServerV2.Library
{
    public class ServerTest
    {
        public ServerTest(String url)
        {
            _url = url;
        }

        private String _url;
        

        public String Test()
        {
            RestClient client = new RestClient(_url);
            var request = new RestRequest("v1/fsuipc/status");
            request.Method = Method.GET;
            request.AddHeader("Content-Type", "application/json");
            var response = client.Execute(request);
            if (response.StatusCode==System.Net.HttpStatusCode.OK)
            {
                return response.Content;
            }
            else
            {
                return "Error";
            }

        }
    }
}
