using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.Descriptions;

namespace Aspid.MVVM.Generators.Generators.Views;

[Generator(LanguageNames.CSharp)]
public partial class ViewGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.ForAttributeWithMetadataName(Classes.ViewAttribute.FullName, SyntacticPredicate, FindView);
        
        context.RegisterSourceOutput(
            source: provider,
            action: static (production, result) =>
            {
                foreach (var diagnostic in result.Diagnostics)
                    production.ReportDiagnostic(diagnostic);
                if (result.Data is { } data) GenerateCode(production, data);
            });
    }

    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        return node is ClassDeclarationSyntax;
    }
}