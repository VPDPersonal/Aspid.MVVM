using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMAnalyzer.FieldAnalyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class BindAttributeAnalyzer : DiagnosticAnalyzer
{
    private const string DiagnosticIdError = "AU0001";
    private const string DiagnosticIdWarning= "AU0002";
    
    private static readonly DiagnosticDescriptor _readRule = new(
        id: DiagnosticIdWarning,
        title: "Field modification restriction",
        messageFormat: "Field '{0}' with attribute '{1}' should be modified using the property '{2}' instead",
        category: "Usage",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);
    
    private static readonly DiagnosticDescriptor _writeRule = new(
        id: DiagnosticIdError,
        title: "Field read restriction",
        messageFormat: "Field '{0}' with attribute '{1}' should be modified using the property '{2}' instead",
        category: "Usage",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);
    
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        ImmutableArray.Create(_readRule, _writeRule);
    
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.IdentifierName);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (IsWithinConstructor(context)) return;
        
        var identifierNode = (IdentifierNameSyntax)context.Node;
        var fieldSymbol = context.SemanticModel.GetSymbolInfo(identifierNode).Symbol as IFieldSymbol;

        if (fieldSymbol == null)
            return;

        var attributeName = string.Empty;
        foreach (var attribute in fieldSymbol.GetAttributes().Select(attribute => attribute.AttributeClass?.ToDisplayString()))
        {
            if (attribute
                is "Aspid.MVVM.ViewModels.Generation.BindAttribute"
                or "Aspid.MVVM.ViewModels.Generation.ReadOnlyBindAttribute")
            {
                attributeName = attribute;
                break;
            }
        }
        
        if (string.IsNullOrEmpty(attributeName)) return;

        var parentNode = identifierNode.Parent;
        var propertyName = ConvertToPascalCase(fieldSymbol.Name);
        
        var rule = parentNode is AssignmentExpressionSyntax assignment && assignment.Left.Equals(identifierNode)
            ? _writeRule
            : _readRule;
        
        context.ReportDiagnostic(Diagnostic.Create(
            rule,
            identifierNode.GetLocation(), 
            fieldSymbol.Name, 
            attributeName,
            propertyName));
    }
    
    private static bool IsWithinConstructor(SyntaxNodeAnalysisContext context) =>
        context.Node.Ancestors().OfType<ConstructorDeclarationSyntax>().Any();
    
    private static string ConvertToPascalCase(string fieldName)
    {
        var name = fieldName.TrimStart('m', '_').TrimStart('_');
        return char.ToUpper(name[0]) + name.Substring(1);
    }
}