using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMAnalyzer.TypeAnalyzers;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class PartialCodeFixProvider : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds =>
        ImmutableArray.Create("AU0003", "AU0004", "AU0005", "AU0006");

    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);
        if (root is null) return;
        
        var diagnostic = context.Diagnostics.First();
        var diagnosticSpan = diagnostic.Location.SourceSpan;
        if (root.FindNode(diagnosticSpan) is not TypeDeclarationSyntax declaration) return;

        context.RegisterCodeFix(
            CodeAction.Create(
                title: "Make partial",
                createChangedDocument: c => AddPartialModifierAsync(context.Document, declaration, c),
                equivalenceKey: "MakePartial"),
            diagnostic);
    }

    private static async Task<Document> AddPartialModifierAsync(Document document, TypeDeclarationSyntax declaration, CancellationToken cancellationToken)
    {
        var newDeclaration = declaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PartialKeyword));
        var root = await document.GetSyntaxRootAsync(cancellationToken);
        var newRoot = root.ReplaceNode(declaration, newDeclaration);
        return document.WithSyntaxRoot(newRoot);
    }
}