using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.Ids;

public static class IdGeneratorExtensions
{
    public static string GetId(this ISymbol member, string memberName)
    {
        if (!member.HasAttribute(Classes.IdAttribute, out var attribute))
            return FieldSymbolExtensions.GetPropertyName(memberName);
        
        var value = attribute!.ConstructorArguments[0].Value as string;
        
        return !string.IsNullOrWhiteSpace(value) 
            ? value! 
            : FieldSymbolExtensions.GetPropertyName(memberName);
    }
}