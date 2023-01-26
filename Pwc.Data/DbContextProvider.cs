using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Pwc.Data
{
    public class DbContextProvider : IDbContextProvider
    {
        public IConfiguration Configuration { get; set; }

        public DbContextProvider(IConfiguration configuration) {
            Configuration = configuration;
        }

        public Context CreateDbContext() {
            try {
                var optionsBuilder = new DbContextOptionsBuilder<Context>();
                var options = optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ConnectionString")).Options;
                return new Context(options);
            }
            catch (Exception) {
                throw;
            }
        }
    }

    public interface IDbContextProvider
    {
        Context CreateDbContext();
    }
}
