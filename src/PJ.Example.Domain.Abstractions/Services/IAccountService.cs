using PJ.Example.Abstractions.Models;
using PJ.Example.Domain.Abstractions.Models;
using PJ.Example.Domain.Abstractions.Models.Account;

namespace PJ.Example.Domain.Abstractions.Services
{
    public interface IAccountService
    {
        Task<AllUsers> GetAllUsers(int? statusId);

        Task<UserDetails> GetUserDetails(string uUid);

        Task<UuidResponse> UpsertUser(UserModel userDetails, string uUid = null);

        Task<List<Role>> GetAllRoles();

        Task<Role> GetRoleDetails(int id);

        Task<IdResponse> UpsertRole(Role request);

        Task AssignRolesToUser(string uUid, IdList roleIds);
    }
}