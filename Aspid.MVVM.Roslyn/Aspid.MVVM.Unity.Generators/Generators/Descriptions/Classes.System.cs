using Aspid.Generator.Helpers;

namespace Aspid.MVVM.Generators.Descriptions;

public static partial class Classes
{
    public static readonly TypeText Span = new("Span", Namespaces.System);
    public static readonly TypeText ReadOnlySpan = new("ReadOnlySpan", Namespaces.System);
    public static readonly TypeText Func = new("Func", Namespaces.System);
    public static readonly TypeText Action = new("Action", Namespaces.System);
    public static readonly TypeText Delegate = new("Delegate", Namespaces.System);
    
    public static readonly TypeText Exception = new("Exception", Namespaces.System);
    public static readonly TypeText ArgumentNullException = new("ArgumentNullException", Namespaces.System);
    public static readonly TypeText InvalidOperationException = new("InvalidOperationException", Namespaces.System);
    
    public static readonly TypeText List = new("List", Namespaces.System_Collections_Generic);
    public static readonly TypeText IList = new("IList", Namespaces.System_Collections_Generic);
    public static readonly TypeText Dictionary = new("Dictionary", Namespaces.System_Collections_Generic);
    public static readonly TypeText IEnumerable = new("IEnumerable", Namespaces.System_Collections_Generic);
    public static readonly TypeText EqualityComparer = new("EqualityComparer", Namespaces.System_Collections_Generic);
    
    public static readonly TypeText EditorBrowsableState = new(nameof(EditorBrowsableState), Namespaces.System_ComponentModel);
    public static readonly AttributeText EditorBrowsableAttribute = new("EditorBrowsable", Namespaces.System_ComponentModel);
    
    public static readonly TypeText MethodImplOptions = new("MethodImplOptions", Namespaces.System_Runtime_CompilerServices);
    public static readonly AttributeText MethodImplAttribute = new("MethodImpl", Namespaces.System_Runtime_CompilerServices);
}