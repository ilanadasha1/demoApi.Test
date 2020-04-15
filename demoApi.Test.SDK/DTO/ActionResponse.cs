using demoApi.Test.SDK;
using demoApi.Test.SDK.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Net;

namespace demoApi.Application.DTO
{
    public class ActionResponse
    {
        public HandlerResponse Content { get; set; }
       
        [JsonConverter(typeof(StringEnumConverter))]
        public HttpStatusCode StatusCode { get; set; }
        public string Error { get; set; } = "None";
        public DateTime Time { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
