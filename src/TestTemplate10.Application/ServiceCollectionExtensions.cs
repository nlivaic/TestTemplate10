using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TestTemplate10.Application.Pipelines;

namespace TestTemplate10.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTestTemplate10ApplicationHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
            services.AddPipelines();

            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
