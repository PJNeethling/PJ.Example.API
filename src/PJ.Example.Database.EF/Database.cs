using Microsoft.EntityFrameworkCore;
using PJ.Example.Database.Abstractions;

namespace PJ.Example.Database.EF
{
    public class Database : BaseDbContext
    {
        public Database(DbContextOptions<Database> options)
            : base(options)
        {
        }
    }
}