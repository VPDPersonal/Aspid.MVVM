using Microsoft.CodeAnalysis;

namespace Aspid.MVVM.Generators.Helpers;

public static class SymbolExtensions
{
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
    
    public static string GetPropertyName(in string name) =>
        Aspid.MVVM.Shared.BindablePropertyNames.FromField(name);

    public static string RemoveFieldPrefix(this ISymbol member) =>
        RemoveFieldPrefix(member.Name);

    public static string RemoveFieldPrefix(in string name) =>
        Aspid.MVVM.Shared.BindablePropertyNames.RemovePrefix(name);

    public static string CapitalizeFirstLetter(this string name) =>
        name.Length == 0 ? name : char.ToUpper(name[0]) + name.Substring(1);
}