using System.Reflection;

namespace CrudBlue.Domain;

public interface IConfigurationDbContext
{
    public List<Assembly> Assemblies { get; set; }
}