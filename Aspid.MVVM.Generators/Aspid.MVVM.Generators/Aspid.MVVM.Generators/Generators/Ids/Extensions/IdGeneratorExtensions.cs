using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using SymbolExtensions = Aspid.MVVM.Generators.Helpers.SymbolExtensions;

namespace Aspid.MVVM.Generators.Generators.Ids.Extensions;

public static class IdGeneratorExtensions
{
    public static string GetId(this ISymbol member, string prefixName = "")
    {
        if (!member.TryGetAnyAttributeInSelf(out var attribute, Classes.IdAttribute))
            return member.GetName(prefixName);
        
        var value = attribute!.ConstructorArguments[0].Value as string;
        
        return !string.IsNullOrWhiteSpace(value) 
            ? value! 
            : member.GetName(prefixName);
    }

    private static string GetName(this ISymbol member, string prefixName) =>
        SymbolExtensions.GetPropertyName(member.Name) + prefixName;
}