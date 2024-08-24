using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Helpers.Descriptions;

namespace MVVMGenerators.Generators.ViewModels;

[Generator(LanguageNames.CSharp)]
public partial class ViewModelGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.ForAttributeWithMetadataName(Classes.ViewModelAttribute.FullName, SyntacticPredicate, FindViewModels).
            Where(foundForSourceGenerator => foundForSourceGenerator.IsNeed).
            Select((foundForSourceGenerator, _) => foundForSourceGenerator.Container);

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

        return candidate is not null
            && candidate.AttributeLists.Count > 0
            && candidate.Modifiers.Any(SyntaxKind.PartialKeyword)
            && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword);
    }
}