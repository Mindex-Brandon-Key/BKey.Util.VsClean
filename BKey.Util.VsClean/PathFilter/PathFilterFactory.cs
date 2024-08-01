using System.Reflection;

namespace BKey.Util.VsClean.PathFilter;
public class PathFilterFactory : IPathFilterFactory
{

    private Lazy<Dictionary<string, Type>> _availableDiscoveryServices = new Lazy<Dictionary<string, Type>>(GetAvailableServices);
    private Dictionary<string, Type> AvailableServices => _availableDiscoveryServices.Value;

    public IPathFilter? CreateService(string optionName)
    {
        if (AvailableServices.TryGetValue(optionName, out var encoderType))
        {
            return (IPathFilter?)Activator.CreateInstance(encoderType);
        }
        return null;
    }

    public IEnumerable<string> ListServices()
    {
        return AvailableServices.Keys;
    }

    private static Dictionary<string, Type> GetAvailableServices()
    {
        var encoderTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(IPathFilter).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToArray();

        var encoders = new Dictionary<string, Type>();

        foreach (var type in encoderTypes)
        {
            var attribute = type.GetCustomAttribute<PathFilterAttribute>();
            var name = attribute != null ? attribute.Name : type.Name;
            encoders[name] = type;
        }

        return encoders;
    }
}