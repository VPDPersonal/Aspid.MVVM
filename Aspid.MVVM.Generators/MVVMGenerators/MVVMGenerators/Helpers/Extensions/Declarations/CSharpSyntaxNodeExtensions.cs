using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Helpers.Extensions.Declarations;

public static class CSharpSyntaxNodeExtensions
{
    public static string GetNamespaceName(this CSharpSyntaxNode node)
    {
        var parent = node.Parent;
        
        while (parent != null)
        {
            if (parent is BaseNamespaceDeclarationSyntax namespaceDeclaration)
                return namespaceDeclaration.Name.ToString();
            
            parent = parent.Parent;
        }

        return "";
    }
}