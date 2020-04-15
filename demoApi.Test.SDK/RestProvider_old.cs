using demoApi.Application.DTO;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace demoApi.Test.SDK
{
    public class RestProvider_old : IRestProvider_Old
    {
        private readonly IRestClient _client;
        private string _userAgent = "Automation";
        public static Dictionary<string, string> DefaultHeaders { get; } = new Dictionary<string, string>
        {
            { "ApplicationIdentifier", "Automation" }
        };

        public RestProvider_old(string apiUrl)
        {
            _client = new RestClient(apiUrl)
            {
                UserAgent = _userAgent
            };
        }




        //public ActionResponse ExecClient(string resource, Method method, Dictionary<string, string> body = null)
        //{
        //    var results = new ActionResponse();
        //    try
        //    {
        //        IRestRequest restRequest = new RestRequest
        //        {
        //            Resource = resource,
        //            Method = method
        //        };
        //        SetBody(restRequest, body);
        //        IRestResponse response = _client.Execute(restRequest);
        //        if (response.Content == null)
        //        {
        //            results.IsSuccedded = false;
        //            results.Reason = "no content";
        //        }
        //        else if (response.StatusCode != HttpStatusCode.OK)
        //        {
        //            results.IsSuccedded = false;
        //            results.Reason = response.StatusCode.ToString();
        //        }
        //        else
        //        {
        //            results = JsonConvert.DeserializeObject<ActionResponse>(response.Content);
        //        }
              
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw ex;
        //    }

        //    return results;
        //}
        //private void SetBody(IRestRequest request, Dictionary<string, string> body)
        //{
        //    if (body != null)
        //    {
        //        foreach (var pair in body)
        //        {
        //            request.AddBody(pair.Key, pair.Value);
        //        }
        //    }
        //}

    }
}
