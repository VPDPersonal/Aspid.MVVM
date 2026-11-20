using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using Aspid.MVVM.Shared;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Analyzers.FieldAnalyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class BindAttributeAnalyzer : DiagnosticAnalyzer
{
    private const string DiagnosticIdError = "AM0001";
    private const string DiagnosticIdWarning= "AM0002";
    
    private static readonly DiagnosticDescriptor _readRule = new(
        id: DiagnosticIdWarning,
        title: "Field read restriction",
        messageFormat: "Field '{0}' with attribute '{1}' should be modified using the property '{2}' instead",
        category: "Usage",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);
    
    private static readonly DiagnosticDescriptor _writeRule = new(
        id: DiagnosticIdError,
        title: "Field modification restriction",
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
        context.RegisterOperationAction(AnalyzeField, OperationKind.FieldReference);
    }

    private static void AnalyzeField(OperationAnalysisContext context)
    {
        if (context.ContainingSymbol is IMethodSymbol { MethodKind: MethodKind.Constructor or MethodKind.StaticConstructor }
            && !context.Operation.Syntax.Ancestors().Any(node => node is AnonymousFunctionExpressionSyntax or LocalFunctionStatementSyntax))
            return;

        var field = (IFieldReferenceOperation)context.Operation;
        var attribute = field.Field.GetAttributes().FirstOrDefault(a => a.AttributeClass?.ToDisplayString() is
            "Aspid.MVVM.BindAttribute" or "Aspid.MVVM.OneWayBindAttribute" or "Aspid.MVVM.TwoWayBindAttribute"
            or "Aspid.MVVM.OneTimeBindAttribute" or "Aspid.MVVM.OneWayToSourceBindAttribute");
        if (attribute is null) return;
        if (field.Parent is INameOfOperation) return;

        var target = (IOperation)field;
        while (target.Parent is IFieldReferenceOperation or IPropertyReferenceOperation or IConversionOperation)
            target = target.Parent;

        var write = target.Parent is IAssignmentOperation assignment && assignment.Target == target
            || target.Parent is IIncrementOrDecrementOperation
            || target.Parent is IArgumentOperation { Parameter.RefKind: RefKind.Ref or RefKind.Out };
        if (target != field && field.Type?.IsValueType != true)
            write = false;

        var propertyName = BindablePropertyNames.FromField(field.Field.Name);
        var canFix = propertyName.Length > 0
            && field.Parent is not IArgumentOperation { Parameter.RefKind: RefKind.Ref or RefKind.Out or RefKind.In }
            && !(write && target != field && field.Type?.IsValueType == true);
        var identifier = field.Syntax switch
        {
            MemberAccessExpressionSyntax access => access.Name,
            IdentifierNameSyntax name => name,
            _ => null
        };
        if (identifier is null) return;

        var properties = ImmutableDictionary<string, string?>.Empty
            .Add("PropertyName", propertyName)
            .Add("CanFix", canFix ? "true" : "false");
        context.ReportDiagnostic(Diagnostic.Create(
            write ? _writeRule : _readRule,
            identifier.GetLocation(),
            properties,
            field.Field.Name,
            attribute.AttributeClass!.ToDisplayString(),
            propertyName));
    }
}
