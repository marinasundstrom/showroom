using System.Threading.Tasks;

using Essiq.Showroom.Server.Data;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Essiq.Showroom.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var sp = scope.ServiceProvider;

                var env = sp.GetService<IHostingEnvironment>();
                if (env.IsDevelopment())
                {
                    var context = sp.GetService<ApplicationDbContext>();

                    await DataSeeder.CreateRolesAndAdminUser(sp, context);
                    DataSeeder.SeedCompetenceAreas(context);
                    DataSeeder.SeedOrganizationsAreas(context);
                    DataSeeder.SeedUserProfileLinkTypes(context);
                    await DataSeeder.SeedDevData(sp, context);
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build())
                .UseStartup<Startup>()
                .Build();
    }
}
