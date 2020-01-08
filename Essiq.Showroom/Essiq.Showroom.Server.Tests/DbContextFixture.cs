using Essiq.Showroom.Server.Data;

using Microsoft.EntityFrameworkCore;

namespace Essiq.Showroom.Server.Tests
{
    public class DbContextFixture
    {
        public ApplicationDbContext CreateDbContext()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("DB");

            return new ApplicationDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
