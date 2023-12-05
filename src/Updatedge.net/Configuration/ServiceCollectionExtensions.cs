using Microsoft.Extensions.DependencyInjection;
using Updatedge.net.Services.V1;

namespace Updatedge.net.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUpdatedgeApi(this IServiceCollection services)
        {
            // register all the Api services
            services.AddTransient<IAvailabilityService, AvailabilityService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<ITimelineService, TimelineService>();
            services.AddTransient<IInviteService, InviteService>();
            services.AddTransient<IWorkerService, WorkerService>();
            services.AddTransient<IOfferService, OfferService>();
            services.AddTransient<IUserService, UserService>();

            return services;

        }
    }
}
