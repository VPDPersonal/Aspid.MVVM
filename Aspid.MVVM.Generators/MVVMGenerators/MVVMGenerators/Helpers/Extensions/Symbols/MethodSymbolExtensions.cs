using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

public static class MethodSymbolExtensions
{
    public static bool EqualsSignature(this IMethodSymbol method1, IMethodSymbol method2)
    {
        if (method1.Parameters.Length != method2.Parameters.Length) return false;
        
        var method1Name = method1.NameFromExplicitImplementation();
        var method2Name = method2.NameFromExplicitImplementation();
        if (method1Name != method2Name) return false;
        
        if (!SymbolEqualityComparer.Default.Equals(method1.ReturnType, method2.ReturnType)) return false;

        var areParametersEqual = !method1.Parameters.Where((parameter, i) =>
            parameter.Type.ToDisplayString() != method2.Parameters[i].Type.ToDisplayString()).Any();
        return areParametersEqual;
    }

    public static string NameFromExplicitImplementation(this IMethodSymbol method) =>
        method.Name.Substring(method.Name.LastIndexOf('.') + 1);
    
    public static string GetParametersAsText(this IMethodSymbol method)
    {
        var text = new StringBuilder();

        foreach (var parameter in method.Parameters)
        {
            if (text.Length > 0)
                text.Append(", ");

            text.Append(parameter.ContainingType.ToDisplayString())
                .Append(" ")
                .Append(parameter.Name);
        }

        return text.ToString();
    }
}