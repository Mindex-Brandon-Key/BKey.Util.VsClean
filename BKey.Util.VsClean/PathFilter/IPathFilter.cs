namespace BKey.Util.VsClean.PathFilter;
public interface IPathFilter
{
    IEnumerable<string> FilterPaths(IEnumerable<string> paths);
}
