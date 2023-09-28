namespace EngineBay.Core
{
    using Microsoft.AspNetCore.DataProtection;

    public interface IModule
    {
        IServiceCollection RegisterModule(IServiceCollection services, IConfiguration configuration);

        IServiceCollection RegisterPolicies(IServiceCollection services);

        RouteGroupBuilder MapEndpoints(RouteGroupBuilder endpoints);

        WebApplication AddMiddleware(WebApplication app);

        IEnumerable<IEnumerable<object>> SeedDatabase(string seedDataPath, IDataProtectionProvider dataProtectorProvider);

        void SeedDatabaseUsingCommand(string seedDataPath, IServiceProvider serviceProvider);

        IEnumerable<string> SeedSqlDatabase(string seedDataPath, IDataProtectionProvider dataProtectorProvider);
    }
}