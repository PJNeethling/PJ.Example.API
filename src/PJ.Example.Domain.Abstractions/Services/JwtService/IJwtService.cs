using PJ.Example.Abstractions.Models;

namespace PJ.Example.Abstractions.Services.JwtService
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(UserAccessInfo user);
    }
}