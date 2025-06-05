using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace MVVMGenerators.Helpers.Extensions.Symbols;

public static partial class SymbolExtensions
{
    public static bool HasAnyAttribute(this ISymbol symbol, params IReadOnlyCollection<AttributeText> attributeText) =>
        symbol.HasAnyAttribute(attributeText.Select(attribute => attribute.FullName).ToArray());
    
    public static bool HasAnyAttribute(this ISymbol symbol, params IReadOnlyCollection<string> attributeFullName)
    {
        foreach (var attribute in symbol.GetAttributes())
        {
            if (attribute.AttributeClass != null && attributeFullName.Any(name => name == attribute.AttributeClass.ToDisplayString()))
                return true;
        }

        return false;
    }

    public static bool HasAnyAttribute(this ISymbol symbol, out AttributeData? foundAttribute, params IReadOnlyCollection<AttributeText> attributeText) =>
        symbol.HasAnyAttribute(out foundAttribute, attributeText.Select(attribute => attribute.FullName).ToArray());

    public static bool HasAnyAttribute(this ISymbol symbol, out AttributeData? foundAttribute, params IReadOnlyCollection<string> attributeFullName)
    {
        foundAttribute = null;

        foreach (var attribute in symbol.GetAttributes())
        {
            if (attribute.AttributeClass != null && attributeFullName.Any(name => name == attribute.AttributeClass.ToDisplayString()))
            {
                foundAttribute = attribute;
                return true;
            }
        }

        return false;
    }
}