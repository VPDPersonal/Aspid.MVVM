using static Aspid.Generators.Helper.Classes;

namespace Aspid.MVVM.Generators.Generators.Descriptions;

public static class Constants
{
    public const string GeneratedCodeIdAttribute =
        "[global::System.CodeDom.Compiler.GeneratedCode(\"Aspid.MVVM.Generators.IdGenerator\", \"1.1.0\")]";
    
    public const string GeneratedCodeViewAttribute =
        "[global::System.CodeDom.Compiler.GeneratedCode(\"Aspid.MVVM.Generators.ViewGenerator\", \"1.1.0\")]";
    
    public const string GeneratedCodeLogBinderAttribute =
        "[global::System.CodeDom.Compiler.GeneratedCode(\"Aspid.MVVM.Generators.LogBinderGenerator\", \"1.1.0\")]";
    
    public const string GeneratedCodeViewModelAttribute =
        "[global::System.CodeDom.Compiler.GeneratedCode(\"Aspid.MVVM.Generators.ViewModelGenerator\", \"1.1.0\")]";
    
    public static readonly string EditorBrowsableAttributeNever = $"[{EditorBrowsableAttribute}({EditorBrowsableState}.Never)]";
}