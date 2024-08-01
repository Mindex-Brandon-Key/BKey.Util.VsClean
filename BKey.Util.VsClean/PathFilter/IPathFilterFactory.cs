
namespace BKey.Util.VsClean.PathFilter;

public interface IPathFilterFactory
{
    IPathFilter? CreateService(string optionName);
    IEnumerable<string> ListServices();
}