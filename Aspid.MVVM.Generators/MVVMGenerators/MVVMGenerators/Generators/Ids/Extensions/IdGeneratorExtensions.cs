using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Ids.Extensions;

public static class IdGeneratorExtensions
{
    public static string GetId(this ISymbol member, string prefixName = "")
    {
        if (!member.HasAttribute(Classes.IdAttribute, out var attribute))
            return member.GetName(prefixName);
        
        var value = attribute!.ConstructorArguments[0].Value as string;
        
        return !string.IsNullOrWhiteSpace(value) 
            ? value! 
            : member.GetName(prefixName);
    }

    private static string GetName(this ISymbol member, string prefixName) =>
        SymbolExtensions.GetPropertyName(member.Name) + prefixName;
}