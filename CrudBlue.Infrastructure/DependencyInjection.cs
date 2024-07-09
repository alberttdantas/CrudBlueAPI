
using CrudBlue.Domain.Repositories;
using CrudBlue.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrudBlue.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddLibs(this IServiceCollection services, ConfigurationDbContext configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(configuration.Assemblies.ToArray());
        });

        services.AddScoped<IContactRepository, ContactRepository>();

        return services;
    }
}
