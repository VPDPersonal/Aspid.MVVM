using MVVMGenerators.Helpers;

namespace MVVMGenerators.Descriptions;

public static partial class Classes
{
    public static readonly TypeText Action = new("Action", Namespaces.System);
    public static readonly TypeText Delegate = new("Delegate", Namespaces.System);
    public static readonly TypeText Exception = new("Exception", Namespaces.System);
    
    public static readonly TypeText List = new("List", Namespaces.System_Collections_Generic);
    public static readonly TypeText Dictionary = new("Dictionary", Namespaces.System_Collections_Generic);
}