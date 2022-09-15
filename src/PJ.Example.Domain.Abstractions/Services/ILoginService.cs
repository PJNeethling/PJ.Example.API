using PJ.Example.Abstractions.Models;

namespace PJ.Example.Abstractions.Services
{
    public interface ILoginService
    {
        Task<AccessTokenLoginResponse> Login(UserLoginRequest userLogin);
    }
}