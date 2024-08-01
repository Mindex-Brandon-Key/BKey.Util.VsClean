using System.Reflection;

namespace BKey.Util.VsClean.Discovery;
public class DiscoveryServiceFactory : IDiscoveryServiceFactory
{

    private Lazy<Dictionary<string, Type>> _availableDiscoveryServices = new Lazy<Dictionary<string, Type>>(GetAvailableServices);
    private Dictionary<string, Type> AvailableServices => _availableDiscoveryServices.Value;

    public IPathDiscoveryService? CreateService(string discoveryType)
    {
        if (AvailableServices.TryGetValue(discoveryType, out var encoderType))
        {
            return (IPathDiscoveryService?)Activator.CreateInstance(encoderType);
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
            .Where(t => typeof(IPathDiscoveryService).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToArray();

        var encoders = new Dictionary<string, Type>();

        foreach (var type in encoderTypes)
        {
            var attribute = type.GetCustomAttribute<DiscoveryServiceAttribute>();
            var name = attribute != null ? attribute.Name : type.Name;
            encoders[name] = type;
        }

        return encoders;
    }
}