using SimpleInjector;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace demoApi.Test.SDK.Logger
{
    public class CustomLogHandler : DelegatingHandler
    {
        private readonly Container _container;
        private readonly ILogHandler _logHandler;
        public CustomLogHandler(Container container, ILogHandler logHandler)
        {
            _logHandler = logHandler;
            _container = container;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var handler = _container.GetInstance<CustomLogHandler>();

            if (!object.ReferenceEquals(handler.InnerHandler, this.InnerHandler))
            {
                handler.InnerHandler = this.InnerHandler;
            }

            // Do not dispose handler as this is managed by Simple Injector
            using (var invoker = new HttpMessageInvoker(handler, disposeHandler: false))
            {
                var logMetadata = BuildRequestMetadata(request);

                var response =  await invoker.SendAsync(request, cancellationToken);
                logMetadata = BuildResponseMetadata(logMetadata, response);
                await SendToLog(logMetadata);
                return response;
            }
        }

        private LogMetadata BuildRequestMetadata(HttpRequestMessage request)
        {
            LogMetadata log = new LogMetadata
            {
                RequestMethod = request.Method.Method,
                RequestTimestamp = DateTime.Now,
                RequestUri = request.RequestUri.ToString()
            };
            return log;
        }
        private LogMetadata BuildResponseMetadata(LogMetadata logMetadata, HttpResponseMessage response)
        {
            logMetadata.ResponseStatusCode = response.StatusCode;
            logMetadata.ResponseTimestamp = DateTime.Now;
            logMetadata.ResponseContentType = response.Content.Headers.ContentType.MediaType;
            return logMetadata;
        }
        private async Task<bool> SendToLog(LogMetadata logMetadata)
        {
            log4net.ThreadContext.Properties["LogMetadata"] = logMetadata;
            _logHandler.Info("Logging metadata");

            return await Task.FromResult(true);
        }
    }
}
