namespace EngineBay.Core
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public abstract class BaseModule : IModule
    {
        public virtual WebApplication AddMiddleware(WebApplication app)
        {
            throw new NotImplementedException();
        }

        public virtual RouteGroupBuilder MapEndpoints(RouteGroupBuilder endpoints)
        {
            throw new NotImplementedException();
        }

        public virtual IServiceCollection RegisterModule(IServiceCollection services, IConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public virtual IServiceCollection RegisterPolicies(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<IEnumerable<object>> SeedDatabase(string seedDataPath, IDataProtectionProvider dataProtectorProvider)
        {
            throw new NotImplementedException();
        }

        public virtual void SeedDatabaseUsingCommand(string seedDataPath, IServiceProvider serviceProvider)
        {
            return;
        }

        public virtual IEnumerable<string> SeedSqlDatabase(string seedDataPath, IDataProtectionProvider dataProtectorProvider)
        {
            return new List<string>();
        }

        protected void LoadSeedData<TDto, TEntity, TCommandHandler>(string seedDataPath, string glob, IServiceProvider serviceProvider)
          where TDto : class
          where TCommandHandler : ICommandHandler<TDto, TEntity>
        {
            var commandHandler = serviceProvider.GetRequiredService<TCommandHandler>();
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            };

            var identity = new ClaimsIdentity(claims, "SeedAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            foreach (string filePath in Directory.EnumerateFiles(seedDataPath, glob, SearchOption.AllDirectories))
            {
                List<TDto>? data = JsonConvert.DeserializeObject<List<TDto>>(File.ReadAllText(filePath));
                if (data is not null)
                {
                    foreach (var entity in data)
                    {
                        _ = commandHandler.Handle(entity, claimsPrincipal, CancellationToken.None).Result;
                    }
                }
            }
        }
    }
}
