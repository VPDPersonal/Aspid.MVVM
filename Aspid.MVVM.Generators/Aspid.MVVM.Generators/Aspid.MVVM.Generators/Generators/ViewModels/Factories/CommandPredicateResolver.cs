using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Generators.ViewModels.Extensions;
using BindMode = Aspid.MVVM.Generators.Generators.ViewModels.Data.BindMode;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

internal static class CommandPredicateResolver
{
    public static bool TryResolve(
        INamedTypeSymbol viewModel,
        IMethodSymbol command,
        string name,
        Compilation compilation,
        out bool isLambda,
        out bool isMethod)
    {
        isLambda = false;
        isMethod = false;
        if (string.IsNullOrWhiteSpace(name)) return false;
        if (!SyntaxFacts.IsValidIdentifier(name) && SyntaxFacts.GetKeywordKind(name) is SyntaxKind.None) return false;
        if (command.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax() is not MethodDeclarationSyntax declaration
            || declaration.Parent is not TypeDeclarationSyntax type) return false;

        var identifier = EscapeIdentifier(name);
        var parameters = string.Join(", ", command.Parameters.Select(parameter =>
            parameter.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)).Concat(new[] { "bool" }));
        var arguments = GetIgnoredParameters(command.Parameters.Length, name);
        var expressions = new[] { identifier, $"({arguments}) => {identifier}()", $"({arguments}) => {identifier}" };
        var annotation = new SyntaxAnnotation();
        var localPrefix = name + "_predicate";
        var probeName = "__aspPredicateProbe";
        while (viewModel.GetMembers(probeName).Length > 0) probeName += "_";
        var probe = SyntaxFactory.ParseMemberDeclaration(
            $"private void {probeName}() {{ " + string.Join(" ", expressions.Select((expression, index) =>
                $"global::System.Func<{parameters}> {localPrefix}{index} = {expression};")) + " }")!
            .WithAdditionalAnnotations(annotation);

        var probeType = type;
        // Generated properties are absent from the input compilation; model their readable bool contract.
        foreach (var field in viewModel.GetMembers().OfType<IFieldSymbol>())
        {
            if (field.IsConst || field.Type.SpecialType is not SpecialType.System_Boolean
                || field.GetBindMode() is BindMode.None || field.GetPropertyName() != name) continue;
            if (viewModel.GetMembers(name).Length > 0) break;
            probeType = probeType.AddMembers(SyntaxFactory.ParseMemberDeclaration($"private bool {identifier} => false;")!);
            break;
        }

        var tree = declaration.SyntaxTree;
        var root = tree.GetRoot().ReplaceNode(type, probeType.AddMembers(probe));
        var probeTree = tree.WithRootAndOptions(root, tree.Options);
        var model = compilation.ReplaceSyntaxTree(tree, probeTree).GetSemanticModel(probeTree);
        var statements = probeTree.GetRoot().GetAnnotatedNodes(annotation).Single()
            .DescendantNodes().OfType<LocalDeclarationStatementSyntax>().ToArray();
        for (var index = 0; index < statements.Length; index++)
        {
            if (model.GetDiagnostics(statements[index].Span).Any(diagnostic => diagnostic.Severity is DiagnosticSeverity.Error))
                continue;

            var value = statements[index].Declaration.Variables.Single().Initializer!.Value;
            var target = value is LambdaExpressionSyntax lambda ? (ExpressionSyntax)lambda.Body : value;
            var symbol = model.GetSymbolInfo(target).Symbol;
            if (index < 2 && symbol is not IMethodSymbol { MethodKind: MethodKind.Ordinary }) continue;
            if (index == 1 && symbol is IMethodSymbol fallback && fallback.Parameters.Length != 0) continue;
            if (index == 2 && symbol is not (IFieldSymbol or IPropertySymbol)) continue;
            var resultType = symbol switch
            {
                IMethodSymbol method => method.ReturnType,
                IFieldSymbol field => field.Type,
                IPropertySymbol property => property.Type,
                _ => null,
            };
            if (resultType?.SpecialType is not SpecialType.System_Boolean) continue;
            isLambda = index > 0;
            isMethod = index < 2;
            return true;
        }

        return false;
    }

    public static string GetIgnoredParameters(int count, string name) =>
        string.Join(", ", Enumerable.Range(0, count).Select(index => name == "_" ? "__aspArgument" + index : "_"));

    public static string EscapeIdentifier(string name) => SyntaxFacts.GetKeywordKind(name) is not SyntaxKind.None
        ? "@" + name
        : name;
}
