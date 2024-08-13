using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

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
    
    public static bool HasAttribute(this ISymbol symbol, AttributeText attributeText, out AttributeData? foundAttribute) =>
        symbol.HasAttribute(attributeText.FullName, out foundAttribute);
    
    public static bool HasAttribute(this ISymbol symbol, string attributeFullName, out AttributeData? foundAttribute)
    {
        foundAttribute = null;
        
        foreach (var attribute in symbol.GetAttributes())
        {
            if (attribute.AttributeClass != null && attribute.AttributeClass.ToDisplayString() == attributeFullName)
            {
                foundAttribute = attribute;
                return true;
            }
        }
        
        return false;
    }
}