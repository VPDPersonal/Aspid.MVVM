using System.Linq;
using Microsoft.CodeAnalysis;

namespace Aspid.Generator.Helpers;

public static class MethodSymbolExtensions
{
    public static bool EqualsSignature(this IMethodSymbol method1, IMethodSymbol method2)
    {
        if (method1.Parameters.Length != method2.Parameters.Length) return false;
        
        var method1Name = method1.NameFromExplicitImplementation();
        var method2Name = method2.NameFromExplicitImplementation();
        if (method1Name != method2Name) return false;
        
        if (!SymbolEqualityComparer.Default.Equals(method1.ReturnType, method2.ReturnType)) return false;

        var areParametersEqual = method1.Parameters
            .Where((parameter, i) => SymbolEqualityComparer.Default.Equals(parameter.Type, method2.Parameters[i].Type))
            .Any();
        
        return areParametersEqual;
    }

    public static string NameFromExplicitImplementation(this IMethodSymbol method) =>
        method.Name.Substring(method.Name.LastIndexOf('.') + 1);
}