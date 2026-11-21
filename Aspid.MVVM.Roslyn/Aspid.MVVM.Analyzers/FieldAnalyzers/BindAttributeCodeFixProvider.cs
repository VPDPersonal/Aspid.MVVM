using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Analyzers.FieldAnalyzers;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class BindAttributeCodeFixProvider : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create("AM0001", "AM0002");
    
    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;
    
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);
        if (root is null) return;
        
        var diagnostic = context.Diagnostics.First();
        
        var diagnosticSpan = diagnostic.Location.SourceSpan;
        if (root.FindNode(diagnosticSpan) is not IdentifierNameSyntax assignment) return;
        
        if (!diagnostic.Properties.TryGetValue("CanFix", out var canFix) || canFix != "true") return;
        if (!diagnostic.Properties.TryGetValue("PropertyName", out var propertyName) || string.IsNullOrEmpty(propertyName)) return;

        context.RegisterCodeFix(
            CodeAction.Create(
                title: $"Use property '{propertyName}'", 
                createChangedDocument: c => UsePropertyInsteadOfFieldAsync(context.Document, assignment, propertyName, c), 
                equivalenceKey: propertyName),
            diagnostic);
    }
    
    private static async Task<Document> UsePropertyInsteadOfFieldAsync(Document document, IdentifierNameSyntax identifier, string propertyName, CancellationToken cancellationToken)
    {
        var escapedName = SyntaxFacts.GetKeywordKind(propertyName) == SyntaxKind.None ? propertyName : "@" + propertyName;
        var propertyAccess = SyntaxFactory.IdentifierName(escapedName).WithTriviaFrom(identifier);
        var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
        var newRoot = root.ReplaceNode(identifier, propertyAccess);
    
        return document.WithSyntaxRoot(newRoot);
    }
    
}
