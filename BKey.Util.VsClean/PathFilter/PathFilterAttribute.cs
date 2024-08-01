namespace BKey.Util.VsClean.PathFilter;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed class PathFilterAttribute : Attribute
{
    public string Name { get; }

    public PathFilterAttribute(string name)
    {
        Name = name;
    }
}
