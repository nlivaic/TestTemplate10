using Microsoft.Extensions.DependencyInjection;
using TestTemplate10.Common.Interfaces;
using TestTemplate10.Core.Interfaces;
using TestTemplate10.Data.Repositories;

namespace TestTemplate10.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSpecificRepositories(this IServiceCollection services) =>
            services.AddScoped<IFooRepository, FooRepository>();

        public static void AddGenericRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
