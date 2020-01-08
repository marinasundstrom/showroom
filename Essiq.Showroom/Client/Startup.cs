using System;
using System.Net.Http;

using AutoMapper;

using Blazor.Extensions.Logging;
using Blazor.Extensions.Storage;

using Essiq.Showroom.Client.Services;
using Essiq.Showroom.Client.Utils;
using Essiq.Showroom.Server.Client;

using Ganss.XSS;

using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Polly;
using Polly.Extensions.Http;

namespace Essiq.Showroom.Client
{
    public class Startup
    {
        private readonly string serviceEndpoint = "https://localhost:44386/";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.AddTokenAuthenticationStateProvider();

            services.AddLogging(builder => builder
                 .AddBrowserConsole()
                 .SetMinimumLevel(LogLevel.Trace)
            );

            services.AddStorage();

            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<IJSHelpers, JSHelpers>();

            services.AddScoped<IHtmlSanitizer, HtmlSanitizer>(x =>
            {
                // Configure sanitizer rules as needed here.
                // For now, just use default rules + allow class attributes
                var sanitizer = new HtmlSanitizer();
                sanitizer.AllowedAttributes.Add("class");
                return sanitizer;
            });

            services.AddSingleton<AppContext>();

            services.AddSingleton<WebAssemblyHttpMessageHandler>();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddHttpClient(nameof(ITokenClient), http =>
            {
                http.BaseAddress = new Uri(serviceEndpoint);
            })
            .AddTypedClient<ITokenClient>((http, sp) => new TokenClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>();

            services.AddHttpClient(nameof(IUserClient), http =>
            {
                http.BaseAddress = new Uri(serviceEndpoint);
            })
            .AddTypedClient<IUserClient>((http, sp) => new UserClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>();

            services.AddHttpClient(nameof(IConsultantProfilesClient), http =>
            {
                http.BaseAddress = new Uri(serviceEndpoint);
            })
            .AddTypedClient<IConsultantProfilesClient>((http, sp) => new ConsultantProfilesClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>();

            services.AddHttpClient(nameof(ICompetenceAreasClient), http =>
            {
                http.BaseAddress = new Uri(serviceEndpoint);
            })
            .AddTypedClient<ICompetenceAreasClient>((http, sp) => new CompetenceAreasClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>();

            services.AddHttpClient(nameof(IOrganizationsClient), http =>
            {
                http.BaseAddress = new Uri(serviceEndpoint);
            })
            .AddTypedClient<IOrganizationsClient>((http, sp) => new OrganizationsClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>();

            services.AddHttpClient(nameof(IManagersClient), http =>
            {
                http.BaseAddress = new Uri(serviceEndpoint);
            })
            .AddTypedClient<IManagersClient>((http, sp) => new ManagersClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>();

            services.AddHttpClient(nameof(IClientProfilesClient), http =>
            {
                http.BaseAddress = new Uri(serviceEndpoint);
            })
            .AddTypedClient<IClientProfilesClient>((http, sp) => new ClientProfilesClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>();

            services.AddHttpClient(nameof(IConsultantRecommendationsClient), http =>
            {
                http.BaseAddress = new Uri(serviceEndpoint);
            })
            .AddTypedClient<IConsultantRecommendationsClient>((http, sp) => new ConsultantRecommendationsClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>();

            services.AddHttpClient(nameof(IClientConsultantInterestsClient), http =>
            {
                http.BaseAddress = new Uri(serviceEndpoint);
            })
            .AddTypedClient<IClientConsultantInterestsClient>((http, sp) => new ClientConsultantInterestsClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>();

            services.AddHttpClient(nameof(IConsultantGalleryClient), http =>
            {
                http.BaseAddress = new Uri(serviceEndpoint);
            })
            .AddTypedClient<IConsultantGalleryClient>((http, sp) => new ConsultantGalleryClient(http)
            {
                RetrieveAuthorizationToken = () => sp.GetService<AppContext>().GetAuthTokenAsync()
            })
            .ConfigurePrimaryHttpMessageHandler<WebAssemblyHttpMessageHandler>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
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
