namespace EngineBay.Core
{
    public interface IModule
    {
        IServiceCollection RegisterModule(IServiceCollection services, IConfiguration configuration);

        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
    }
}