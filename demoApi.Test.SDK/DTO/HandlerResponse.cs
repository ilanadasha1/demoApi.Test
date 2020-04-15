using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace demoApi.Test.SDK.DTO
{
    public class HandlerResponse
    {
        public Position? position { get; set; }
        public string Type { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CommandStatus Status { get; set; }
        public string ErrorReason { set; get; } = "None";

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
