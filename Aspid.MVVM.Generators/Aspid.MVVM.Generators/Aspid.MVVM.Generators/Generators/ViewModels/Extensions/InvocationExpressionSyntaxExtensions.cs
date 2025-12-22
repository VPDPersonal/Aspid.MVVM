using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Extensions;

public static class InvocationExpressionSyntaxExtensions
{
    public static string? GetMethodName(this InvocationExpressionSyntax invocation) => invocation.Expression switch
    {
        IdentifierNameSyntax identifier => identifier.Identifier.Text,
        MemberAccessExpressionSyntax memberAccess => memberAccess.Name.Identifier.Text,
        _ => null
    };
}
