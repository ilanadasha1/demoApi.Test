using demoApi.Application.DTO;
using demoApi.Concrete;
using demoApi.Test.SDK.DTO;
using demoApi.Test.SDK.Logger;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace demoApi.Test.SDK
{
    public class HandlerApiSDK : IHandlerApiSDK
    {
        private readonly string HandlerApiUrl = "https://localhost:44389";
        private readonly IRestProvider _restProvider;
        private readonly ILogHandler _logHandler;

        public HandlerApiSDK(IRestProvider restProvider, ILogHandler logHandler)
        {
            _restProvider = restProvider;
            _logHandler = logHandler;
        }

        public async Task<ActionResponse> SetHandlerPosition(int X, int Y, int Z)
        {
            _logHandler.Info($"Set Handler Position cordinates to: {X},{Y},{Z}");

            var actionRequest = new ActionRequest() { position = new Position() { X = X, Y = Y, Z = Z } };
            var request = new RestRequest($"/Move", Method.POST);
            var data = JsonConvert.SerializeObject(actionRequest);

            request.AddParameter("application/json", data, ParameterType.RequestBody);
            var response =  await _restProvider.ExecuteAsync(HandlerApiUrl, request);

            //ResponseHandler(response, "SetHandlerPosition");
            return response;
        }

        public async Task<ActionResponse> LockHandlerPosition()
        {
            _logHandler.Info($"LockHandlerPosition");
            
            var request = new RestRequest($"/Lock", Method.GET);
            var response =  await _restProvider.ExecuteAsync(HandlerApiUrl, request);

            //return ResponseHandler(response, "LockHandlerPosition");
            return response;
        }

        public async Task<ActionResponse> UnlockHandlerPosition()
        {
            _logHandler.Info($"UnlockHandlerPosition");

            var request = new RestRequest($"/UnLock", Method.GET);
            var response =  await _restProvider.ExecuteAsync(HandlerApiUrl, request);

            //return ResponseHandler(response, "UnlockHandlerPosition");
            return response;
        }

        public async Task<ActionResponse> SaveHandlerPosition()
        {
            _logHandler.Info($"SaveHandlerPosition");

            var request = new RestRequest($"/Save", Method.GET);
            var response = await _restProvider.ExecuteAsync(HandlerApiUrl, request);

            //return ResponseHandler(response, "SaveHandlerPosition");
            return response;
        }

        public async Task<ActionResponse> GetHandlerPosition(Guid positionId)
        {
            _logHandler.Info($"GetHandlerPosition, PositionId: {positionId}");

            var request = new RestRequest($"/Get", Method.POST);
            var data = JsonConvert.SerializeObject(new ActionRequest() { position = new Position() { PositionId = positionId } });

            request.AddParameter("application/json", data, ParameterType.RequestBody);
            var response =  await _restProvider.ExecuteAsync(HandlerApiUrl, request);

            //return ResponseHandler(response, "GetHandlerPosition");
            return response;
        }

        public async Task<ActionResponse> GetCurrentHandlerPosition()
        {
            _logHandler.Info($"GetCurrentHandlerPosition");

            var request = new RestRequest($"/Get", Method.GET);
            var response = await _restProvider.ExecuteAsync(HandlerApiUrl, request);

            //return ResponseHandler(response, "GetCurrentHandlerPosition");
            return response;
        }

        //private ActionResponse ResponseHandler(ActionResponse response, string message)
        //{
        //    if (response.StatusCode != HttpStatusCode.OK)
        //        _logHandler.Error($"{message} - Failed");
        //    else
        //        _logHandler.Info($"{message} - OK");
            
        //    return response;
        //}
    }
}
