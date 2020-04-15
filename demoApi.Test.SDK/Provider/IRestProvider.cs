using demoApi.Application.DTO;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace demoApi.Test.SDK
{
    public interface IRestProvider
    {
        Task<ActionResponse> ExecuteAsync(string baseUrl, IRestRequest request, string userAgent = "HandlerAutomation", int timeout = 2000);
    }
}