using Microsoft.EntityFrameworkCore;
using Pwc.Domain;

namespace Pwc.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base((DbContextOptions)options) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
        }

        public virtual DbSet<Client> Clients { get; set; }
    }
}
