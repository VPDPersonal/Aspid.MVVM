using MVVMGenerators.Helpers;

namespace MVVMGenerators.Descriptions;

public static partial class Classes
{
    public static readonly TypeText Action = new("Action", Namespaces.System);
    public static readonly TypeText Exception = new("Exception", Namespaces.System);
    public static readonly TypeText List = new("List", Namespaces.System_Collections_Generic);
}