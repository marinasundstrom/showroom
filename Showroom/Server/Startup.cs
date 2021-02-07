using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Showroom.Server.Configuration;
using Showroom.Server.Data;
using Showroom.Server.Models;
using Showroom.Server.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using NJsonSchema;

using NSwag;
using NSwag.Generation.Processors.Security;

namespace Showroom.Server
{
    public class Startup
    {
        private readonly IHostEnvironment env;

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;

            this.env = env;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddNewtonsoftJson();

            services.AddDbContext<ApplicationDbContext>
                (options => _ = env.IsDevelopment() ?
                    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
                        : options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<User>(options =>
            {
                options.ClaimsIdentity.UserIdClaimType = JwtRegisteredClaimNames.Sub;
                //options.SignIn.RequireConfirmedEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(ClaimTypes.NameIdentifier);
                });
            });

            var jwtConfig = Configuration.GetSection("Jwt").Get<JwtConfiguration>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidAudience = jwtConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            //if (!string.IsNullOrEmpty(accessToken) &&
                            //    (context.HttpContext.WebSockets.IsWebSocketRequest || context.Request.Headers["Accept"] == "text/event-stream"))
                            //{
                            //    context.Token = context.Request.Query["access_token"];
                            //}
                            // If the request is for our hub...

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddResponseCaching();

            services.AddHttpContextAccessor();

            services.AddOpenApiDocument();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddTransient<IJwtTokenService, JwtTokenService>();

            services.AddScoped<IImageService, ImageService>();

            services.AddScoped<IImageResizer, ImageResizer>();

            services.AddScoped<IVideoStreamService, VideoStreamService>();

            services.AddScoped<IImageUploader, ImageUploader>();

            services.AddScoped<IVideoUploader, VideoUploader>();

            services.AddScoped<IEmailSender, EmailSender>();

            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddScoped<ClientManager>();

            services.AddScoped<ConsultantManager>();

            services.AddScoped<ManagerManager>();

            services.AddScoped<IProfileImageService, ProfileImageService>();

            services.AddScoped<IProfileVideoService, ProfileVideoService>();

            services.AddScoped<RecommendationService>();

            services.AddScoped<InterestsService>();

            services.AddAutoMapper((serviceProvider, automapper) =>
            {
                //automapper.UseEntityFrameworkCoreModel<ApplicationDbContext>(serviceProvider);
            },
            typeof(Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCaching();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });      
        }
    }
}
