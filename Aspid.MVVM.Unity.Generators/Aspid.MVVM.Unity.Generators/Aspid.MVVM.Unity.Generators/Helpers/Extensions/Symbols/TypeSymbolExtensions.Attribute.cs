using Microsoft.CodeAnalysis;

namespace Aspid.Generator.Helpers;

public static partial class TypeSymbolExtensions
{
    public static bool HasAttributeInBases(this ITypeSymbol typeSymbol, AttributeText attributeText) =>
        HasAttributeInBases(typeSymbol, attributeText.FullName);

    public static bool HasAttributeInBases(this ITypeSymbol symbol, string fullName)
    {
        for (var type = symbol.BaseType; type is not null; type = type.BaseType)
        {
            if (type.HasAnyAttribute(fullName))
                return true;
        }

        return false;
    }

    public static bool HasAttributeInBases(this ITypeSymbol typeSymbol, AttributeText attributeText, out AttributeData? attribute) =>
        HasAttributeInBases(typeSymbol, attributeText.FullName, out attribute);

    public static bool HasAttributeInBases(this ITypeSymbol symbol, string fullName, out AttributeData? attribute)
    {
        for (var type = symbol.BaseType; type is not null; type = type.BaseType)
        {
            if (type.HasAnyAttribute(out attribute, fullName))
                return true;
        }

        attribute = null;
        return false;
    }

    public static bool HasAttributeInSelfOrBases(this ITypeSymbol typeSymbol, AttributeText attributeText) =>
        HasAttributeInSelfOrBases(typeSymbol, attributeText.FullName);

    public static bool HasAttributeInSelfOrBases(this ITypeSymbol symbol, string fullName)
    {
        for (var type = symbol; type is not null; type = type.BaseType)
        {
            if (type.HasAnyAttribute(fullName))
                return true;
        }

        return false;
    }

    public static bool HasAttributeInSelfOrBases(this ITypeSymbol typeSymbol, AttributeText attributeText, out AttributeData? attribute) =>
        HasAttributeInSelfOrBases(typeSymbol, attributeText.FullName, out attribute);

    public static bool HasAttributeInSelfOrBases(this ITypeSymbol symbol, string fullName, out AttributeData? attribute)
    {
        for (var type = symbol; type is not null; type = type.BaseType)
        {
            if (type.HasAnyAttribute(out attribute, fullName))
                return true;
        }

        attribute = null;
        return false;
    }
}