using Microsoft.EntityFrameworkCore;
using PJ.Example.Database.Abstractions.Models;
using PJ.Example.Database.Abstractions.Queries;

namespace PJ.Example.Database.Abstractions
{
    public abstract partial class BaseDbContext : IDatabase
    {
        public async Task<List<RoleQuery>> GetAllRoles()
        {
            return await Roles.AsNoTracking().ToListAsync();
        }

        public async Task<RoleQuery> GetRoleDetails(int id)
        {
            return await Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IdQuery> CreateRole(RoleQuery request)
        {
            await Roles.AddAsync(request);

            await SaveChangesAsync();

            return new IdQuery { Id = request.Id.Value };
        }

        public async Task UpdateRole(RoleQuery request)
        {
            Roles.Update(request);

            await SaveChangesAsync();
        }
    }
}