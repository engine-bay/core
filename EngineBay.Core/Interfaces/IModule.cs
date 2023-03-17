namespace EngineBay.Core
{
    public interface IModule
    {
        IServiceCollection RegisterModule(IServiceCollection services);

        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
    }
}