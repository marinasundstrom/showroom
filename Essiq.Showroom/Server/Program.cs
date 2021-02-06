using System.Threading.Tasks;

using Essiq.Showroom.Server.Data;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Essiq.Showroom.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var sp = scope.ServiceProvider;

                var env = sp.GetService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                {
                    var context = sp.GetService<ApplicationDbContext>();

                    await context.Database.EnsureCreatedAsync();

                    try
                    {
                        await context.Database.MigrateAsync();
                    }
                    catch
                    {

                    }

                    await DataSeeder.CreateRolesAndAdminUser(sp, context);
                    DataSeeder.SeedCompetenceAreas(context);
                    DataSeeder.SeedOrganizationsAreas(context);
                    DataSeeder.SeedUserProfileLinkTypes(context);
                    await DataSeeder.SeedDevData(sp, context);
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
