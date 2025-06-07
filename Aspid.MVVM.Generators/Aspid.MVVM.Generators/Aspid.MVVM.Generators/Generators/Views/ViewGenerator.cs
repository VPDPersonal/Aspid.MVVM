using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Generators.Views;

[Generator(LanguageNames.CSharp)]
public partial class ViewGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.ForAttributeWithMetadataName(Classes.ViewAttribute.FullName, SyntacticPredicate, FindView)
            .Where(foundForSourceGenerator => foundForSourceGenerator.IsNeed)
            .Select((foundForSourceGenerator, _) => foundForSourceGenerator.Container);
        
        context.RegisterSourceOutput(
            source: provider,
            action: GenerateCode);
    }

    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        // TODO add support for static
        return node is ClassDeclarationSyntax candidate
            && candidate.Modifiers.Any(SyntaxKind.PartialKeyword)
            && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword);
    }
}