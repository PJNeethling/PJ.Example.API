using PJ.Example.Database.Abstractions.Models;
using PJ.Example.Database.Abstractions.ProcedureParamaters;
using PJ.Example.Database.Abstractions.Queries;
using PJ.Example.Domain.Abstractions.Models;

namespace PJ.Example.Database.Abstractions
{
    public interface IDatabase
    {
        Task<UserLoginQuery> Login(string emailOrUsername, string passphrase);

        Task<List<UsersQuery>> GetAllUsers(int? statusId, string passphrase);

        Task<UsersDetailsQuery> GetUserDetails(string uUid, string passphrase);

        Task<List<RoleQuery>> GetAllRoles();

        Task<RoleQuery> GetRoleDetails(int id);

        Task<IdQuery> CreateRole(RoleQuery request);

        Task UpdateRole(RoleQuery request);

        Task<UuidQuery> UpsertUser(UserParams userParams);

        Task AssignRolesToUser(Guid uUid, IdList roleIds);
    }
}