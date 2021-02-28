using System;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Showroom.Application.Services;

namespace Showroom.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);

            services.AddAutoMapper((serviceProvider, automapper) =>
            {
                //automapper.UseEntityFrameworkCoreModel<ApplicationDbContext>(serviceProvider);
            },
            typeof(ServiceCollectionExtensions).Assembly);

            services.AddScoped<IImageService, ImageService>();

            services.AddScoped<IImageResizer, ImageResizer>();

            services.AddScoped<IVideoStreamService, VideoStreamService>();

            services.AddScoped<IImageUploader, ImageUploader>();

            services.AddScoped<IVideoUploader, VideoUploader>();

            services.AddScoped<IEmailSender, EmailSender>();

            services.AddScoped<IProfileImageService, ProfileImageService>();

            services.AddScoped<IProfileVideoService, ProfileVideoService>();

            services.AddScoped<RecommendationService>();

            services.AddScoped<InterestsService>();

            return services;
        }
    }
}
