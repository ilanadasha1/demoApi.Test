using demoApi.Application.DTO;
using demoApi.Concrete;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace demoApi.Test.SDK
{
    public class MathApiSDK : IMathApiSDK
    {
        private readonly string MathApiUrl = "https://localhost:44389";
        private readonly IRestProvider _restProvider;

        public MathApiSDK(IRestProvider restProvider)
        {
            _restProvider = restProvider;
        }


        public async Task<ActionResponse> AddNumbers(object num1, object num2)
        {
            var request = new RestRequest($"/Add", Method.POST);
            var data = JsonConvert.SerializeObject(new
            {
                numbers = new List<float>() { (float)num1, (float)num2}
            });

           request.AddParameter("application/json", data, ParameterType.RequestBody);
           return await _restProvider.ExecuteAsync(MathApiUrl, request, "MathApiSDK");
        }

        public async Task<ActionResponse> DevideNumbers(object num1, object num2)
        {
            var request = new RestRequest($"/Devide", Method.POST);
            var data = JsonConvert.SerializeObject(new
            {
                numbers = new List<float>() { (float)num1, (float)num2 }
            });

            request.AddParameter("application/json", data, ParameterType.RequestBody);
            return await _restProvider.ExecuteAsync(true, MathApiUrl, request, "MathApiSDK");
        }
    }
}
