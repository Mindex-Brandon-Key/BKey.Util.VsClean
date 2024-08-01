namespace BKey.Util.VsClean.Removal;
public class DeleteServiceDryRun : IDeleteService
{
    public void RemovePaths(IEnumerable<string> paths)
    {
        Console.WriteLine("Dry Run: The following paths would be deleted:");
        foreach (var path in paths)
        {
            Console.WriteLine(path);
        }
    }
}