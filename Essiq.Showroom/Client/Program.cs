using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Blazored.LocalStorage;
using Essiq.Showroom.Client.Services;
using Essiq.Showroom.Client.Utils;
using Essiq.Showroom.Server.Client;

using Ganss.XSS;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Polly;
using Polly.Extensions.Http;

namespace Essiq.Showroom.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddAuthorizationCore();
            builder.Services.AddTokenAuthenticationStateProvider();

            builder.Services.AddLogging(builder => builder
                 .SetMinimumLevel(LogLevel.Trace));

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddSingleton<IJSHelpers, JSHelpers>();

            builder.Services.AddScoped<IHtmlSanitizer, HtmlSanitizer>(x =>
            {
                // Configure sanitizer rules as needed here.
                // For now, just use default rules + allow class attributes
                var sanitizer = new HtmlSanitizer();
                sanitizer.AllowedAttributes.Add("class");
                return sanitizer;
            });

            builder.Services.AddScoped<AppContext>();

            builder.Services.AddScoped<IIdentityService, IdentityService>();

            builder.Services.AddHttpClient(nameof(ITokenClient), (sp, http) =>
            {
                http.BaseAddress = new Uri(sp.GetRequiredService<NavigationManager>().BaseUri);
            })
            .AddTypedClient<ITokenClient>((http, sp) => new TokenClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            });

            builder.Services.AddHttpClient(nameof(IUserClient), (sp, http) =>
            {
                http.BaseAddress = new Uri(sp.GetRequiredService<NavigationManager>().BaseUri);
            })
            .AddTypedClient<IUserClient>((http, sp) => new UserClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            });

            builder.Services.AddHttpClient(nameof(IConsultantProfilesClient), (sp, http) =>
            {
                http.BaseAddress = new Uri(sp.GetRequiredService<NavigationManager>().BaseUri);
            })
            .AddTypedClient<IConsultantProfilesClient>((http, sp) => new ConsultantProfilesClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            });

            builder.Services.AddHttpClient(nameof(ICompetenceAreasClient), (sp, http) =>
            {
                http.BaseAddress = new Uri(sp.GetRequiredService<NavigationManager>().BaseUri);
            })
            .AddTypedClient<ICompetenceAreasClient>((http, sp) => new CompetenceAreasClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            });

            builder.Services.AddHttpClient(nameof(IOrganizationsClient), (sp, http) =>
            {
                http.BaseAddress = new Uri(sp.GetRequiredService<NavigationManager>().BaseUri);
            })
            .AddTypedClient<IOrganizationsClient>((http, sp) => new OrganizationsClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            });

            builder.Services.AddHttpClient(nameof(IManagersClient), (sp, http) =>
            {
                http.BaseAddress = new Uri(sp.GetRequiredService<NavigationManager>().BaseUri);
            })
            .AddTypedClient<IManagersClient>((http, sp) => new ManagersClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            });

            builder.Services.AddHttpClient(nameof(IClientProfilesClient), (sp, http) =>
            {
                http.BaseAddress = new Uri(sp.GetRequiredService<NavigationManager>().BaseUri);
            })
            .AddTypedClient<IClientProfilesClient>((http, sp) => new ClientProfilesClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            });

            builder.Services.AddHttpClient(nameof(IConsultantRecommendationsClient), (sp, http) =>
            {
                http.BaseAddress = new Uri(sp.GetRequiredService<NavigationManager>().BaseUri);
            })
            .AddTypedClient<IConsultantRecommendationsClient>((http, sp) => new ConsultantRecommendationsClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            });

            builder.Services.AddHttpClient(nameof(IClientConsultantInterestsClient), (sp, http) =>
            {
                http.BaseAddress = new Uri(sp.GetRequiredService<NavigationManager>().BaseUri);
            })
            .AddTypedClient<IClientConsultantInterestsClient>((http, sp) => new ClientConsultantInterestsClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            });

            builder.Services.AddHttpClient(nameof(IConsultantGalleryClient), (sp, http) =>
            {
                http.BaseAddress = new Uri(sp.GetRequiredService<NavigationManager>().BaseUri);
            })
            .AddTypedClient<IConsultantGalleryClient>((http, sp) => new ConsultantGalleryClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            });

            await builder.Build().RunAsync();
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
