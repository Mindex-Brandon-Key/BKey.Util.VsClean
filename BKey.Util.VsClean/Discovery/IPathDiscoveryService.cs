namespace BKey.Util.VsClean.Discovery;
public interface IPathDiscoveryService
{
    IEnumerable<string> CollectPaths(string root);
}
