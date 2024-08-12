using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

public static class NamedSymbolExtensions
{
    public static bool HasBaseType(this ITypeSymbol symbol, TypeText typeText, out INamedTypeSymbol? foundBaseType) =>
        symbol.HasBaseType(typeText.FullName, out foundBaseType);

    public static bool HasBaseType(this ITypeSymbol symbol, string baseTypeName, out INamedTypeSymbol? foundBaseType)
    {
       var result = symbol.HasBaseType(baseTypeName, out ITypeSymbol? baseType);
       foundBaseType = baseType as INamedTypeSymbol;
       return result;
    }
}