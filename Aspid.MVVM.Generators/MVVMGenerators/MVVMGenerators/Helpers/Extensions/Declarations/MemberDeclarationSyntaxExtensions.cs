using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Helpers.Extensions.Declarations;

public static class MemberDeclarationSyntaxExtensions
{
    public static bool HasAttribute(this MemberDeclarationSyntax declaration, SemanticModel semanticModel, string name)
    {
        foreach (var attribute in declaration.AttributeLists.SelectMany(attributeList => attributeList.Attributes))
        {
            if (semanticModel.GetSymbolInfo(attribute).Symbol is not IMethodSymbol attributeSymbol) continue;
            if (attributeSymbol.ContainingType?.ToDisplayString() == name) return true;
        }

        return false;
    }
}