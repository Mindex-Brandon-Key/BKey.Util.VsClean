
namespace BKey.Util.VsClean.Discovery;

public interface IDiscoveryServiceFactory
{
    IPathDiscoveryService? CreateService(string encodingType);
    IEnumerable<string> ListServices();
}