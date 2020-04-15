using demoApi.Application.DTO;
using demoApi.Concrete;
using System;
using System.Threading.Tasks;

namespace demoApi.Test.SDK
{
    public interface IHandlerApiSDK
    {
        Task<ActionResponse> SetHandlerPosition(int X, int Y, int Z);
        Task<ActionResponse> LockHandlerPosition();
        Task<ActionResponse> UnlockHandlerPosition();
        Task<ActionResponse> SaveHandlerPosition();
        Task<ActionResponse> GetHandlerPosition(Guid positionId);
        Task<ActionResponse> GetCurrentHandlerPosition();
    }
}