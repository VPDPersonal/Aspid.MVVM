using System.Linq;
using System.Globalization;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.Views.Data.Members;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.Views.Factories;

internal static class AsBinderFactory
{
    private static readonly DiagnosticDescriptor InvalidType = new(
        "ASPIDVIEW001", "Invalid AsBinder type", "'{0}': {1}", "Aspid.MVVM", DiagnosticSeverity.Error, true);
    private static readonly DiagnosticDescriptor InvalidArguments = new(
        "ASPIDVIEW002", "Invalid AsBinder arguments", "'{0}': {1}", "Aspid.MVVM", DiagnosticSeverity.Error, true);

    public static AsBinderMember? Create(
        ISymbol member,
        ITypeSymbol targetType,
        AttributeData attribute,
        SemanticModel semanticModel,
        out Diagnostic? diagnostic)
    {
        diagnostic = null;
        var location = attribute.ApplicationSyntaxReference?.GetSyntax().GetLocation() ?? member.Locations.FirstOrDefault();
        if (attribute.ConstructorArguments.FirstOrDefault().Value is not INamedTypeSymbol binderType
            || binderType.IsAbstract || binderType.IsStatic || binderType.IsUnboundGenericType
            || !binderType.HasAnyInterfaceInSelfAndBases(Classes.IBinder)
            || !semanticModel.Compilation.IsSymbolAccessibleWithin(binderType, member.ContainingType))
        {
            diagnostic = Diagnostic.Create(InvalidType, location, member.Name,
                "The binder type must be accessible, non-abstract, closed and implement IBinder.");
            return null;
        }

        if (member is IPropertySymbol property && (property.GetMethod is null || property.IsIndexer))
        {
            diagnostic = Diagnostic.Create(InvalidArguments, location, member.Name,
                "The target must be a readable, non-indexed property.");
            return null;
        }

        var arguments = new List<string>();
        if (attribute.ConstructorArguments.Length > 1)
        {
            var values = attribute.ConstructorArguments[1];
            if (values.IsNull || values.Kind is not TypedConstantKind.Array)
            {
                diagnostic = Diagnostic.Create(InvalidArguments, location, member.Name,
                    "The constructor argument array cannot be null.");
                return null;
            }

            var expressions = ArgumentExpressions(attribute);
            for (var i = 0; i < values.Values.Length; i++)
            {
                var value = values.Values[i];
                if (value.Kind is TypedConstantKind.Error)
                {
                    diagnostic = Diagnostic.Create(InvalidArguments, location, member.Name,
                        "A constructor argument is not a valid attribute constant.");
                    return null;
                }

                var isNameOf = i < expressions.Count && expressions[i] is InvocationExpressionSyntax invocation
                    && invocation.Expression is IdentifierNameSyntax { Identifier.ValueText: "nameof" };
                arguments.Add(isNameOf && value.Value is string name ? Escape(name) : Format(value));
            }
        }

        var typeName = binderType.ToDisplayStringGlobal();
        if (member.DeclaringSyntaxReferences.FirstOrDefault() is { } declaration)
        {
            var model = semanticModel.Compilation.GetSemanticModel(declaration.SyntaxTree);
            var parameters = new[] { $"default({targetType.ToDisplayStringGlobal()})" }.Concat(arguments);
            var creation = SyntaxFactory.ParseExpression($"new {typeName}({string.Join(", ", parameters)})");
            var constructor = model.GetSpeculativeSymbolInfo(declaration.Span.Start, creation, SpeculativeBindingOption.BindAsExpression);
            if (constructor.Symbol is not IMethodSymbol)
            {
                diagnostic = Diagnostic.Create(InvalidArguments, location, member.Name,
                    "No accessible constructor accepts the target followed by the supplied arguments.");
                return null;
            }
        }

        return new AsBinderMember(member, typeName, arguments);
    }

    private static string Escape(string name) =>
        SyntaxFacts.GetKeywordKind(name) is not SyntaxKind.None ? "@" + name : name;

    private static string Format(TypedConstant value)
    {
        if (value.IsNull) return "null";
        if (value.Kind is TypedConstantKind.Type && value.Value is ITypeSymbol type)
            return $"typeof({type.ToDisplayStringGlobal()})";
        if (value.Kind is TypedConstantKind.Array)
            return $"new {value.Type!.ToDisplayStringGlobal()} {{ {string.Join(", ", value.Values.Select(Format))} }}";
        if (value.Kind is TypedConstantKind.Enum)
            return $"({value.Type!.ToDisplayStringGlobal()}){SymbolDisplay.FormatPrimitive(value.Value!, true, false)}";
        if (value.Value is float single)
            return FormatReal(single, "Single", single.ToString("R", CultureInfo.InvariantCulture) + "F");
        if (value.Value is double number)
            return FormatReal(number, "Double", number.ToString("R", CultureInfo.InvariantCulture) + "D");
        if (value.Value is not (string or char or bool))
            return $"({value.Type!.ToDisplayStringGlobal()})({value.ToCSharpString()})";
        return value.ToCSharpString();
    }

    private static string FormatReal(double value, string type, string literal)
    {
        if (double.IsNaN(value)) return $"global::System.{type}.NaN";
        if (double.IsPositiveInfinity(value)) return $"global::System.{type}.PositiveInfinity";
        if (double.IsNegativeInfinity(value)) return $"global::System.{type}.NegativeInfinity";
        return literal;
    }

    private static IReadOnlyList<ExpressionSyntax> ArgumentExpressions(AttributeData attribute)
    {
        var result = new List<ExpressionSyntax>();
        if (attribute.ApplicationSyntaxReference?.GetSyntax() is not AttributeSyntax syntax) return result;
        if (syntax.ArgumentList is null) return result;

        for (var i = 0; i < syntax.ArgumentList.Arguments.Count; i++)
        {
            var argument = syntax.ArgumentList.Arguments[i];
            var name = argument.NameColon?.Name.Identifier.ValueText;
            if (argument.NameEquals is not null || name == "type" || name is null && i is 0) continue;

            var expression = argument.Expression;
            var initializer = expression switch
            {
                ArrayCreationExpressionSyntax array => array.Initializer,
                ImplicitArrayCreationExpressionSyntax array => array.Initializer,
                _ => null
            };
            if (initializer is not null && (name == "arguments" || syntax.ArgumentList.Arguments.Count is 2))
                result.AddRange(initializer.Expressions);
            else
                result.Add(expression);
        }

        return result;
    }
}
