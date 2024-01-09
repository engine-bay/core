namespace EngineBay.Core
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class BaseModule : IModule
    {
        public virtual WebApplication AddMiddleware(WebApplication app)
        {
            return app;
        }

        public virtual RouteGroupBuilder MapEndpoints(RouteGroupBuilder endpoints)
        {
            return endpoints;
        }

        public virtual IServiceCollection RegisterModule(IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public virtual IServiceCollection RegisterPolicies(IServiceCollection services)
        {
            return services;
        }

        public virtual void SeedDatabase(string seedDataPath, IServiceProvider serviceProvider)
        {
            return;
        }
    }
}
