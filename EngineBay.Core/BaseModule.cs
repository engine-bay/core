namespace EngineBay.Core
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

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

        protected void LoadSeedData<TInputParameters, TOutputDto, TCommandHandler>(
            string seedDataPath,
            string glob,
            IServiceProvider serviceProvider)
            where TInputParameters : class
            where TCommandHandler : ICommandHandler<TInputParameters, TOutputDto>
        {
            var commandHandler = serviceProvider.GetRequiredService<TCommandHandler>();

            if (Directory.Exists(seedDataPath))
            {
                foreach (string filePath in Directory.EnumerateFiles(seedDataPath, glob, SearchOption.AllDirectories))
                {
                    List<TInputParameters>? data =
                        JsonConvert.DeserializeObject<List<TInputParameters>>(File.ReadAllText(filePath));
                    if (data is not null)
                    {
                        foreach (var entity in data)
                        {
                            _ = commandHandler.Handle(entity, CancellationToken.None).Result;
                        }
                    }
                }
            }
        }

        protected void LoadSeedData<TInputParameters, TOutputDto, TCommandHandler>(
            TInputParameters[] seedData,
            IServiceProvider serviceProvider)
            where TInputParameters : class
            where TCommandHandler : ICommandHandler<TInputParameters, TOutputDto>
        {
            ArgumentNullException.ThrowIfNull(seedData);

            var commandHandler = serviceProvider.GetRequiredService<TCommandHandler>();

            foreach (var entity in seedData)
            {
                _ = commandHandler.Handle(entity, CancellationToken.None).Result;
            }
        }

        protected void LoadSeedData<TInputParameters, TModuleDbContext>(
           string seedDataPath,
           string glob,
           IServiceProvider serviceProvider)
           where TInputParameters : BaseModel
           where TModuleDbContext : DbContext
        {
            var dbContext = serviceProvider.GetRequiredService<TModuleDbContext>();

            if (Directory.Exists(seedDataPath))
            {
                foreach (string filePath in Directory.EnumerateFiles(seedDataPath, glob, SearchOption.AllDirectories))
                {
                    List<TInputParameters>? data = JsonConvert.DeserializeObject<List<TInputParameters>>(File.ReadAllText(filePath));

                    if (data is not null)
                    {
                        dbContext.AddRange(data);
                        dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
