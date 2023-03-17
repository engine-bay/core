namespace EngineBay.Core
{
    using System.Reflection;

    public static class ModuleRegistration
    {
        private static readonly List<IModule> RegisteredModules = new List<IModule>();

        public static IServiceCollection RegisterModules2(this IServiceCollection services)
        {
            var modules = DiscoverModules();
            foreach (var module in modules)
            {
                module.RegisterModule(services);
                RegisteredModules.Add(module);
            }

            return services;
        }

        public static WebApplication MapEndpoints(this WebApplication app)
        {
            foreach (var module in RegisteredModules)
            {
                module.MapEndpoints(app);
            }

            return app;
        }

        private static IEnumerable<IModule> DiscoverModules()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var modules = assembly
                .GetTypes()
                .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
                .Select(Activator.CreateInstance)
                .Cast<IModule>();

            Console.WriteLine($"Discovered {modules.Count()} modules");
            return modules;
        }
    }
}