using PJ.Example.Abstractions.Models;
using PJ.Example.Abstractions.Repositories;
using PJ.Example.Abstractions.Services;
using PJ.Example.Abstractions.Services.JwtService;

namespace PJ.Example.Domain
{
    public class LoginService : ILoginService
    {
        private readonly IJwtService _jwtService;
        private readonly ILoginRepository _repo;

        public LoginService(IJwtService jwtService, ILoginRepository repo)
        {
            _jwtService = jwtService;
            _repo = repo;
        }

        public async Task<AccessTokenLoginResponse> Login(UserLoginRequest userLogin)
        {
            var loginResponse = await _repo.UserLogin(userLogin);

            return new AccessTokenLoginResponse
            {
                Token = await _jwtService.GenerateJwtToken(new UserAccessInfo { Uuid = loginResponse.UUid, Roles = loginResponse.Roles }),
                UUid = loginResponse.UUid
            };
        }
    }
}