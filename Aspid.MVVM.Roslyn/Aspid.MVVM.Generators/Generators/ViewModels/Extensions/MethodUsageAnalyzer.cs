using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Extensions;

public static class MethodUsageAnalyzer
{
    public static HashSet<string> GetUsedMethods(
        TypeDeclarationSyntax declaration,
        HashSet<string> methodNames,
        CancellationToken cancellationToken)
    {
        var usedMethods = new HashSet<string>();

        foreach (var invocation in declaration.DescendantNodes().OfType<InvocationExpressionSyntax>())
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (invocation.Ancestors().OfType<TypeDeclarationSyntax>().First() != declaration) continue;
            if (invocation.Expression is MemberAccessExpressionSyntax access && access.Expression is not ThisExpressionSyntax)
                continue;
            var invokedMethodName = invocation.GetMethodName();
            
            if (invokedMethodName is not null && methodNames.Contains(invokedMethodName))
                usedMethods.Add(invokedMethodName);
        }

        return usedMethods;
    }
}