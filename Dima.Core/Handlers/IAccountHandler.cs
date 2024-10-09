using Dima.Api.Response;
using Dima.Core.Requests.Account;

namespace Dima.Core.Handlers
{
    public interface IAccountHandler
    {
        Task<Resposta<string>> LoginAsync(LoginRequest request);
        Task<Resposta<string>> RegisterAsync(RegisterRequest request);
        Task LogoutAsync();
    }
}
