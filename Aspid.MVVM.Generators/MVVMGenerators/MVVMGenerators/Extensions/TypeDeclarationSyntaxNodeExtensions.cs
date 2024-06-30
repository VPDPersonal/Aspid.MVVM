using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Extensions;

public static class TypeDeclarationSyntaxNodeExtensions
{
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