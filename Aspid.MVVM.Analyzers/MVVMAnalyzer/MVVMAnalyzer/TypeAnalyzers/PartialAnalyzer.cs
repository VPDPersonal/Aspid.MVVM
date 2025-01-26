using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMAnalyzer.TypeAnalyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PartialAnalyzer : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor _classViewRule = new(
        id: "AU0003",
        title: "Class with View attribute should be partial",
        messageFormat: "Class '{0}' with View attribute must be declared as partial",
        category: "Usage",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);
    
    private static readonly DiagnosticDescriptor _structViewRule = new(
        id: "AU0004",
        title: "Struct with View attribute should be partial",
        messageFormat: "Struct '{0}' with View attribute must be declared as partial",
        category: "Usage",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);
    
    private static readonly DiagnosticDescriptor _classViewModelRule = new(
        id: "AU0005",
        title: "Class with ViewModel attribute should be partial",
        messageFormat: "Class '{0}' with ViewModel attribute must be declared as partial",
        category: "Usage",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);
    
    private static readonly DiagnosticDescriptor _structViewModelRule = new(
        id: "AU0006",
        title: "Struct with ViewModel attribute should be partial",
        messageFormat: "Struct '{0}' with ViewModel attribute must be declared as partial",
        category: "Usage",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);
    
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        ImmutableArray.Create(_classViewRule, _structViewRule, _classViewModelRule, _structViewModelRule);
    
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration, SyntaxKind.StructDeclaration);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        var declaration = (TypeDeclarationSyntax)context.Node;
        
        foreach (var attribute in declaration.AttributeLists.SelectMany(a => a.Attributes).Take(1))
        {
            if (declaration.Modifiers.Any(SyntaxKind.PartialKeyword)) continue;
            if (context.SemanticModel.GetSymbolInfo(attribute).Symbol is not IMethodSymbol attributeSymbol) continue;
            
            if (attributeSymbol.ContainingType.ToDisplayString() == "Aspid.MVVM.Generation.ViewModelAttribute")
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    declaration is ClassDeclarationSyntax ? _classViewModelRule : _structViewModelRule,
                    declaration.Identifier.GetLocation(),
                    declaration.Identifier.Text));
            }
            else if (attributeSymbol.ContainingType.ToDisplayString() == "Aspid.MVVM.Generation.ViewAttribute")
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    declaration is ClassDeclarationSyntax ? _classViewRule : _structViewRule,
                    declaration.Identifier.GetLocation(),
                    declaration.Identifier.Text));
            }
        }
    }
}