using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;

namespace MVVMGenerators.Extensions.Symbols;

public static class SymbolExtensions
{
    public static bool HasAttribute(this ISymbol symbol, AttributeText attributeText) =>
        symbol.HasAttribute(attributeText.FullName);
    
    public static bool HasAttribute(this ISymbol symbol, string attributeFullName)
    {
        foreach (var attribute in symbol.GetAttributes())
        {
            if (attribute.AttributeClass != null && attribute.AttributeClass.ToDisplayString() == attributeFullName)
                return true;
        }
        
        return false;
    }
}