using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Extensions;

public static class MethodUsageAnalyzer
{
    public static HashSet<string> GetUsedMethods(TypeDeclarationSyntax declaration, HashSet<string> methodNames)
    {
        var usedMethods = new HashSet<string>();

        foreach (var invocation in declaration.DescendantNodes().OfType<InvocationExpressionSyntax>())
        {
            var invokedMethodName = GetMethodName(invocation);
            
            if (invokedMethodName is not null && methodNames.Contains(invokedMethodName))
                usedMethods.Add(invokedMethodName);
        }

        return usedMethods;
    }
    
    private static string? GetMethodName(InvocationExpressionSyntax invocation) => invocation.Expression switch
    {
        IdentifierNameSyntax identifier => identifier.Identifier.Text,
        MemberAccessExpressionSyntax memberAccess => memberAccess.Name.Identifier.Text,
        _ => null
    };
}

