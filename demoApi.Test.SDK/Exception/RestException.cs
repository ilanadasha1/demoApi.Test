using RestSharp;
using System;
using System.Runtime.Serialization;

namespace demoApi.Test.SDK
{
    [Serializable]
    public class RestException : Exception
    {
        public RestException()
        {
        }

        public RestException(string message) : base(message)
        {
        }

        public RestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IRestResponse Response { get; set; }
    }
}