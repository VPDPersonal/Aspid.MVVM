using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;

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
    
    public static IReadOnlyList<ISymbol> FillMembers(
        this INamedTypeSymbol symbol,
        in IList<IFieldSymbol>? fields = null,
        in IList<IMethodSymbol>? methods = null,
        in IList<IPropertySymbol>? properties = null)
    {
        return FillMembers(symbol.GetMembers(), fields, methods, properties);
    }
    
    public static IReadOnlyList<ISymbol> FillMembers(
        this ImmutableArray<ISymbol> members,
        in IList<IFieldSymbol>? fields = null,
        in IList<IMethodSymbol>? methods = null,
        in IList<IPropertySymbol>? properties = null)
    {
        var index = 0;
        var otherMembers = new List<ISymbol>(members);

        foreach (var member in members)
        {
            switch (member)
            {
                case IFieldSymbol field:
                    if (fields != null)
                    {
                        fields.Add(field);
                        otherMembers.RemoveAt(index);
                        index -= 1;
                    }
                    break;
                
                case IMethodSymbol method:
                    if (methods != null)
                    {
                        methods.Add(method);
                        otherMembers.RemoveAt(index);
                        index -= 1;
                    }
                    break;
                
                case IPropertySymbol property:
                    if (properties != null)
                    {
                        properties.Add(property);
                        otherMembers.RemoveAt(index);
                        index -= 1;
                    }
                    break;
            }

            index++;
        }

        return otherMembers;
    }
}