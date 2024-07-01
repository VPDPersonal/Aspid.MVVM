using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Extensions.Symbols;

public static class SymbolExtensions
{
    public static bool HasAttribute(this ISymbol symbol, string attributeName)
    {
        foreach (var attribute in symbol.GetAttributes())
        {
            if (attribute.AttributeClass != null && attribute.AttributeClass.Name == attributeName)
                return true;
        }
        
        return false;
    }
}