using Xunit;

namespace Showroom.Server.Tests
{

    public class ConsultantManagerTest : IClassFixture<DbContextFixture>
    {
        private readonly DbContextFixture fixture;

        public ConsultantManagerTest(DbContextFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Test1()
        {
            using (var dbContext = fixture.CreateDbContext())
            {
                //var foo = new ConsultantManager(dbContext, );
            }
        }
    }
}
