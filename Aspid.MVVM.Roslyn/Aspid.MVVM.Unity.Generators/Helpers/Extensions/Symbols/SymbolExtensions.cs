using Microsoft.CodeAnalysis;

namespace Aspid.Generator.Helpers;

public static partial class SymbolExtensions
{
    public static string ToDisplayStringGlobal(this ISymbol symbol) =>
        symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    
    public static ITypeSymbol? GetSymbolType(this ISymbol symbol) => symbol switch
    {
        ITypeSymbol type => type,
        IFieldSymbol field => field.Type,
        ILocalSymbol local => local.Type,
        IEventSymbol @event => @event.Type,
        IDiscardSymbol discard => discard.Type,
        
        // TODO Delete
        IMethodSymbol method => method.ReturnType,
        
        IPropertySymbol property => property.Type,
        IParameterSymbol parameter => parameter.Type,
        _ => null
    };
    
    public static string GetFieldName(this ISymbol member, in string? prefix = "_") =>
        GetFieldName(member.Name, prefix);
    
    public static string GetFieldName(string name, in string? prefix = "_")
    {
        if (name.Length is 0) return name;
        
        var firstSymbol = name[0];
        var isFirstSymbolUpper = char.IsUpper(firstSymbol);

        if (isFirstSymbolUpper)
        {
            name = name.Remove(0, 1);
            name = char.ToLower(firstSymbol) + name;
        }
        
        if (!string.IsNullOrWhiteSpace(prefix))
            return prefix + name;
        
        return name;
    }
    
    public static string GetPropertyName(this ISymbol member) =>
        GetPropertyName(member.Name);
    
    public static string GetPropertyName(in string name)
    {
        var newName = RemoveFieldPrefix(name);
        return char.ToUpper(newName[0]) + newName.Substring(1);
    }
    
    public static string RemoveFieldPrefix(this ISymbol member) =>
        RemoveFieldPrefix(member.Name);

    public static string RemoveFieldPrefix(in string name)
    {
        var prefixCount = name.StartsWith("_") 
            ? 1
            : name.StartsWith("m_") || name.StartsWith("s_")  
                ? 2 
                : 0;
        
        return prefixCount > 0 
            ? name.Remove(0, prefixCount) 
            : name;
    }
}