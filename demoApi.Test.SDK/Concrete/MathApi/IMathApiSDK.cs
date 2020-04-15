using demoApi.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demoApi.Test.SDK
{
    public interface IMathApiSDK
    {
        Task<ActionResponse> AddNumbers(object num1, object num2);
        Task<ActionResponse> DevideNumbers(object num1, object num2);
    }
}