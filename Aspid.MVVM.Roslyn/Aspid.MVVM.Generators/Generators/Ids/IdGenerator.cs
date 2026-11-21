using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Generators.Generators.Ids;

[Generator(LanguageNames.CSharp)]
public partial class IdGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // ReSharper disable once NullableWarningSuppressionIsUsed
        var provider = context.SyntaxProvider.CreateSyntaxProvider(SyntacticPredicate, GetIdsForSourceGeneration)
            .Where(foundFor => foundFor is not null)
            .Select((foundFor, _) => foundFor!);
        
        context.RegisterSourceOutput(provider.Collect(), GenerateCode);
    }

    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        var candidate = node switch
        {
            ClassDeclarationSyntax => node as TypeDeclarationSyntax,
            _ => null
        };

        return candidate is not null
            && candidate.AttributeLists.Count > 0
            && candidate.Modifiers.Any(SyntaxKind.PartialKeyword)
            && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword);
    }
}