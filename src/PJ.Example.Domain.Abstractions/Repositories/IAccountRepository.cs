using PJ.Example.Abstractions.Models;
using PJ.Example.Domain.Abstractions.Models;
using PJ.Example.Domain.Abstractions.Models.Account;

namespace PJ.Example.Abstractions.Repositories
{
    public interface IAccountRepository
    {
        Task<AllUsers> GetAllUsers(int? statusId);

        Task<UserDetails> GetUserDetails(string uUid);

        Task<UuidResponse> UpsertUser(UserModel userDetails, string uUid = null);

        Task AssignRolesToUser(string uUid, IdList roleIds);

        Task<List<Role>> GetAllRoles();

        Task<Role> GetRoleDetails(int id);

        Task<IdResponse> UpsertRole(Role request);
    }
}