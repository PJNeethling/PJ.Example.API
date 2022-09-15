using PJ.Example.Abstractions.Models;

namespace PJ.Example.Abstractions.Repositories
{
    public interface ILoginRepository
    {
        Task<LoginResponse> UserLogin(UserLoginRequest request);

        //Task<IdQuery> UpsertUser(UserModel userDetails);
    }
}