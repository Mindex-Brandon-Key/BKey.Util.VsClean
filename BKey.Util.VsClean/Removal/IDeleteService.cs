namespace BKey.Util.VsClean.Removal;
public interface IDeleteService
{
    void RemovePaths(IEnumerable<string> paths);
}
