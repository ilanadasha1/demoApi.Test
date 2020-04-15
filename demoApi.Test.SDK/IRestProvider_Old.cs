using demoApi.Application.DTO;
using RestSharp;
using System.Collections.Generic;

namespace demoApi.Test.SDK
{
    public interface IRestProvider_Old
    {
        ActionResponse ExecClient(string resource, Method method, Dictionary<string, string> body = null);
    }
}