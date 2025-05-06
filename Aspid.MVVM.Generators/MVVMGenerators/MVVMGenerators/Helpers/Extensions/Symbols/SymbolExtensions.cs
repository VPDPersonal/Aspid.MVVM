using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

public static class SymbolExtensions
{
    public static ITypeSymbol? GetSymbolType(this ISymbol symbol) => symbol switch
    {
        ITypeSymbol type => type,
        IFieldSymbol field => field.Type,
        ILocalSymbol local => local.Type,
        IEventSymbol @event => @event.Type,
        IDiscardSymbol discard => discard.Type,
        IMethodSymbol method => method.ReturnType,
        IPropertySymbol property => property.Type,
        IParameterSymbol parameter => parameter.Type,
        _ => null
    };
    
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

    public static string GetPropertyName(this ISymbol member) =>
        FieldSymbolExtensions.GetPropertyName(member.Name);
    
    public static string GetFieldName(this ISymbol member, bool hasPrefix = true)
    {
        if (member is IFieldSymbol field)
            return hasPrefix ? field.RemovePrefix() : field.Name;

        return PropertySymbolExtensions.GetFieldName(member.Name, hasPrefix);
    }
}