#nullable enable

using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Generators.Shared;

internal sealed class AspidTypeDeclaration
{
    private static readonly SymbolDisplayFormat TypeFormat = SymbolDisplayFormat.FullyQualifiedFormat
        .WithMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers
            | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier
            | SymbolDisplayMiscellaneousOptions.UseSpecialTypes);

    internal static readonly DiagnosticDescriptor InvalidContainer = new(
        "ASPIDGEN001", "Unsupported containing type", "Cannot generate '{0}': containing type '{1}' must be partial and cannot be file-local",
        "Aspid.MVVM", DiagnosticSeverity.Error, true);

    internal INamedTypeSymbol Symbol { get; }
    internal IReadOnlyList<INamedTypeSymbol> Containers { get; }
    internal string Namespace { get; }

    internal AspidTypeDeclaration(INamedTypeSymbol symbol)
    {
        Symbol = symbol;
        var containers = new Stack<INamedTypeSymbol>();
        for (var parent = symbol.ContainingType; parent is not null; parent = parent.ContainingType)
            containers.Push(parent);
        Containers = containers.ToArray();
        var namespaces = new Stack<string>();
        for (var ns = symbol.ContainingNamespace; !ns.IsGlobalNamespace; ns = ns.ContainingNamespace)
            namespaces.Push(Escape(ns.Name));
        Namespace = string.Join(".", namespaces);
    }

    internal static Diagnostic? ValidateContainers(INamedTypeSymbol symbol)
    {
        for (var parent = symbol.ContainingType; parent is not null; parent = parent.ContainingType)
        {
            foreach (var reference in parent.DeclaringSyntaxReferences)
            {
                if (reference.GetSyntax() is not TypeDeclarationSyntax declaration) continue;
                if (!declaration.Modifiers.Any(SyntaxKind.PartialKeyword)
                    || declaration.Modifiers.Any(modifier => modifier.ValueText == "file"))
                    return Diagnostic.Create(InvalidContainer, declaration.Identifier.GetLocation(), symbol.Name, parent.Name);
            }
        }
        return null;
    }

    internal string GetFileName(string? postfix) =>
        Identity(Symbol) + (string.IsNullOrEmpty(postfix) ? "" : "." + Encode(postfix!)) + ".g.cs";

    internal static string Identity(INamedTypeSymbol symbol)
    {
        var names = new Stack<string>();
        for (var type = symbol; type is not null; type = type.ContainingType)
            names.Push(type.MetadataName);
        var ns = symbol.ContainingNamespace.IsGlobalNamespace
            ? ""
            : symbol.ContainingNamespace.ToDisplayString() + ".";
        return Encode(ns + string.Join("+", names));
    }

    internal static string Encode(string value)
    {
        var builder = new StringBuilder();
        foreach (var character in value)
        {
            if (character is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or >= '0' and <= '9' or '.' or '`')
                builder.Append(character);
            else
                builder.Append('_').Append(((int)character).ToString("X4"));
        }
        return builder.ToString();
    }

    internal static string Identifier(string value) =>
        Encode(value).Replace(".", "_002E").Replace("`", "_0060");

    internal static string Escape(string name) => SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None
        || SyntaxFacts.GetContextualKeywordKind(name) != SyntaxKind.None
            ? "@" + name
            : name;

    internal static string Header(
        INamedTypeSymbol symbol,
        IEnumerable<string>? bases = null,
        string? name = null,
        bool isPartial = true)
    {
        var modifiers = new List<string>();
        if (symbol.IsStatic)
            modifiers.Add("static");
        if (symbol.IsReadOnly)
            modifiers.Add("readonly");
        if (symbol.IsRefLikeType)
            modifiers.Add("ref");
        if (isPartial)
            modifiers.Add("partial");
        modifiers.Add(symbol.IsRecord
            ? symbol.TypeKind == TypeKind.Struct ? "record struct" : "record"
            : symbol.TypeKind == TypeKind.Interface ? "interface" : symbol.TypeKind == TypeKind.Struct ? "struct" : "class");
        var parameters = symbol.TypeParameters.Select(parameter =>
            (parameter.Variance == VarianceKind.In ? "in " : parameter.Variance == VarianceKind.Out ? "out " : "")
            + Escape(parameter.Name)).ToArray();
        var header = string.Join(" ", modifiers) + " " + Escape(name ?? symbol.Name)
            + (parameters.Length == 0 ? "" : "<" + string.Join(", ", parameters) + ">");
        if (bases is not null)
        {
            var types = bases.ToArray();
            if (types.Length > 0)
                header += " : " + string.Join(", ", types);
        }
        foreach (var parameter in symbol.TypeParameters)
        {
            var constraints = new List<string>();
            if (parameter.HasUnmanagedTypeConstraint)
                constraints.Add("unmanaged");
            else if (parameter.HasValueTypeConstraint)
                constraints.Add("struct");
            else if (parameter.HasReferenceTypeConstraint)
                constraints.Add(parameter.ReferenceTypeConstraintNullableAnnotation == NullableAnnotation.Annotated ? "class?" : "class");
            else if (parameter.HasNotNullConstraint)
                constraints.Add("notnull");
            constraints.AddRange(parameter.ConstraintTypes.Select(type => type.ToDisplayString(TypeFormat)));
            if (parameter.HasConstructorConstraint)
                constraints.Add("new()");
            if (constraints.Count > 0)
                header += " where " + Escape(parameter.Name) + " : " + string.Join(", ", constraints);
        }
        return header;
    }
}
