using System.Reflection;

namespace BartugWeb.PersistanceLayer;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}