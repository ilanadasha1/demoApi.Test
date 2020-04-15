using demoApi.Application.DTO;
using demoApi.Test.SDK.DTO;
using demoApi.Test.SDK.Logger;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace demoApi.Test.SDK
{
    public class RestProvider : IRestProvider
    {
        private readonly ILogHandler _logHandler;

        public RestProvider(ILogHandler logHandler)
        {
            _logHandler = logHandler;
        }

        public async Task<ActionResponse> ExecuteAsync(string baseUrl, IRestRequest request, string userAgent = "HandlerAutomation", int timeout = 2000)
        {
            var res = new ActionResponse();
            try
            {
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;

                var client = new RestClient { BaseUrl = new Uri(baseUrl), FollowRedirects = false, Timeout = timeout > 0 ? timeout : 2000 };
                client.UserAgent = userAgent;

                _logHandler.Info($"Send {request.Method} to endpoint {request.Resource}");

                var response = await client.ExecuteAsync(request);
                var responseContent = JsonConvert.DeserializeObject<HandlerResponse>(response.Content);

                if (response.ErrorException == null && (int)response.StatusCode < 400)
                {
                    res =  new ActionResponse()
                    {
                        Content = responseContent,
                        StatusCode = response.StatusCode
                    };

                    _logHandler.Info("Response", res.Content);
                }
                else
                {
                    res = new ActionResponse()
                    {
                        StatusCode = response != null ? response.StatusCode : HttpStatusCode.InternalServerError,
                        Error = responseContent != null
                        ? responseContent?.ErrorReason
                        : (response != null ? response.ErrorException.Message?.ToString() : "Uknown Erorr"),
                        Content = responseContent,
                    };
                    
                    _logHandler.Error("Bad response", res);
                }
            }
            catch (Exception ex)
            {
                res = new ActionResponse()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Error = $"Unknown Exception: {((RestException)ex)?.InnerException}, Message: {((RestException)ex)?.Message}",
                    Content = null
                };
                _logHandler.Error("Exception", res);
            }
            return res; 
        }
    }

}
