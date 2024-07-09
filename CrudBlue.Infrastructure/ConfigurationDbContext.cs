
using CrudBlue.Domain;
using System.Reflection;

namespace CrudBlue.Infrastructure;

#nullable disable
public class ConfigurationDbContext : IConfigurationDbContext
{
    public List<Assembly> Assemblies { get; set; } = new();
}