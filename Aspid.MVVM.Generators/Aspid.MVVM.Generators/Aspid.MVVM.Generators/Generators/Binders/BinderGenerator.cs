using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Generators.Generators.Binders;

[Generator]
public partial class BinderGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // ReSharper disable once NullableWarningSuppressionIsUsed
        var provider = context.SyntaxProvider.CreateSyntaxProvider(SyntacticPredicate, FindBinders)
            .Where(data => data.HasValue)
            .Select((data, _) => data!.Value);
        
        context.RegisterSourceOutput(
            source: provider,
            action: GenerateCode);
    }

    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        var candidate = node switch
        {
            ClassDeclarationSyntax or StructDeclarationSyntax => node as TypeDeclarationSyntax,
            _ => null
        };

        // Must implement IBinder or inherit from a type that inherits IBinder
        return candidate is not null
            && candidate.Modifiers.Any(SyntaxKind.PartialKeyword)
            && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword)
            && candidate.BaseList is { Types.Count: > 0 };
    }
}