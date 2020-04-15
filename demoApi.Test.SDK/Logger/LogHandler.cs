using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace demoApi.Test.SDK.Logger
{
    public class LogHandler : ILogHandler
    {
        private readonly ILog _logger;


        public LogHandler(ILog logger)
        {
            _logger = logger;

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = { new StringEnumConverter() }
            };
        }

        public void Info(string message = null, object obj = null)
        {
            if (obj != null)
            {
                _logger.Info(obj);
            }
            else
                _logger.Info(message);
        }

        public void Error(string message = null, object obj = null)
        {
            if (obj != null)
                _logger.Error(obj);
            else
                _logger.Error(message);
        }
    }
}
