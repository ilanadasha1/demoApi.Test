using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace demoApi.Test.SDK.DTO
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommandStatus
    {
        Error,
        Ack
    }
}
