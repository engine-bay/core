namespace EngineBay.Core
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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

        protected void LoadSeedData<TInputParameters, TLinkedType, TModuleDbContext>(
            string seedDataPath,
            string glob,
            IServiceProvider serviceProvider,
            string linkedKey,
            Func<IEnumerable<string>, IEnumerable<TLinkedType>> linkedDataFinder)
            where TInputParameters : BaseModel
            where TLinkedType : BaseModel
            where TModuleDbContext : DbContext
        {
            ArgumentNullException.ThrowIfNull(linkedDataFinder);

            var dbContext = serviceProvider.GetRequiredService<TModuleDbContext>();

            // var linkedKey = typeof(TLinkedType).Name.Pluralize();
            if (Directory.Exists(seedDataPath))
            {
                foreach (string filePath in Directory.EnumerateFiles(seedDataPath, glob, SearchOption.AllDirectories))
                {
                    var models = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(File.ReadAllText(filePath));

                    if (models is not null)
                    {
                        foreach (Dictionary<string, object> model in models)
                        {
                            List<string>? values;

                            if (model[linkedKey] is JArray)
                            {
                                values = JsonConvert.DeserializeObject<List<string>>(model[linkedKey].ToString() ?? string.Empty);
                            }
                            else
                            {
                                values = new List<string> { model[linkedKey].ToString() ?? string.Empty };
                            }

                            if (values != null)
                            {
                                List<TLinkedType> relatedValues = linkedDataFinder(values).ToList();

                                // model.Remove(linkedKey);
                                model[linkedKey] = relatedValues;
                                var data = JsonConvert.DeserializeObject<TInputParameters>(JsonConvert.SerializeObject(model));

                                if (data is not null)
                                {
                                    // var property = data.GetType().GetProperty(linkedKey);
                                    // if (property != null)
                                    // {
                                    //    property.SetValue(data, relatedValues);
                                    // }
                                    dbContext.Add(data);
                                }
                            }
                        }
                    }

                    dbContext.SaveChanges();
                }
            }
        }
    }
}
