using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.Generator.Helpers;

public static class CSharpSyntaxNodeExtensions
{
    public static string GetNamespaceName(this CSharpSyntaxNode node)
    {
        for (var parent = node.Parent; parent != null; parent = parent.Parent)
        {
            if (parent is BaseNamespaceDeclarationSyntax namespaceDeclaration)
                return namespaceDeclaration.Name.ToString();
        }

        return "";
    }
}