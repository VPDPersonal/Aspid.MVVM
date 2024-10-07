using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMAnalyzer.FieldAnalyzers;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class BindAttributeCodeFixProvider : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create("AU0001", "AU0002");
    
    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;
    
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);
        if (root is null) return;
        
        var diagnostic = context.Diagnostics.First();
        
        var diagnosticSpan = diagnostic.Location.SourceSpan;
        if (root.FindNode(diagnosticSpan) is not IdentifierNameSyntax assignment) return;
        
        var fieldName = assignment.Identifier.Text;
        
        var propertyName = diagnostic.Properties.TryGetValue("PropertyName", out var property)
            ? property!
            : ConvertToPascalCase(fieldName);
        
        context.RegisterCodeFix(
            CodeAction.Create(
                title: $"Use property '{propertyName}'", 
                createChangedDocument: c => UsePropertyInsteadOfFieldAsync(context.Document, assignment, propertyName, c), 
                equivalenceKey: propertyName),
            diagnostic);
    }
    
    private static async Task<Document> UsePropertyInsteadOfFieldAsync(Document document, IdentifierNameSyntax identifier, string propertyName, CancellationToken cancellationToken)
    {
        var propertyAccess = SyntaxFactory.IdentifierName(propertyName);
        var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
        var newRoot = root.ReplaceNode(identifier, propertyAccess);
    
        return document.WithSyntaxRoot(newRoot);
    }

    private static string ConvertToPascalCase(string fieldName)
    {
        var name = fieldName.TrimStart('m', '_').TrimStart('_');
        return char.ToUpper(name[0]) + name.Substring(1);
    }
}