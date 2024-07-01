using System.Text;
using MVVMGenerators.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Extensions.Declarations;

public static class TypeDeclarationSyntaxExtensions
{
    public static DeclarationText GetDeclarationText(this TypeDeclarationSyntax declaration)
    {
        var modifiers = declaration.GetModifiersAsText();
        var typeType = declaration is ClassDeclarationSyntax ? "class" : "struct";
        var typeName = declaration.Identifier.Text;
        var genericArguments = declaration.GetGenericArgumentsAsText();
        
        return new DeclarationText(modifiers, typeType, typeName, genericArguments);
    }
    
    public static string GetModifiersAsText(this TypeDeclarationSyntax declaration)
    {
        var modifiers = new StringBuilder();
        foreach (var modifier in declaration.Modifiers)
            modifiers.Append(modifier.ToString() + " ");

        modifiers.Length--;
        return modifiers.ToString();
    }
    
    public static string GetGenericArgumentsAsText(this TypeDeclarationSyntax declaration)
    {
        var types = declaration.TypeParameterList;
        if (types == null || types.Parameters.Count == 0) return "";
        
        var genericTypes = new StringBuilder();
        foreach (var type in types.Parameters)
        {
            if (genericTypes.Length != 1)
                genericTypes.Append(", ");
            
            genericTypes.Append(type.Identifier.Text);
        }
        
        return genericTypes.ToString();
    }
}