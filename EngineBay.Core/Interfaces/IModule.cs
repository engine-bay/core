namespace EngineBay.Core
{
    using Microsoft.AspNetCore.DataProtection;

    public interface IModule
    {
        IServiceCollection RegisterModule(IServiceCollection services, IConfiguration configuration);

        IServiceCollection RegisterPolicies(IServiceCollection services);

        RouteGroupBuilder MapEndpoints(RouteGroupBuilder endpoints);

        WebApplication AddMiddleware(WebApplication app);

        void SeedDatabase(string seedDataPath, IServiceProvider serviceProvider);
    }
}