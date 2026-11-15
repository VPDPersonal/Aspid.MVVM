using System.Threading;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.Views.Data;
using Aspid.MVVM.Generators.Generators.Views.Factories;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.Views;

public partial class ViewGenerator
{
    private static readonly DiagnosticDescriptor InvalidView = new(
        "ASPIDVIEW003", "Invalid View declaration", "View '{0}' must be a non-static partial class",
        "Aspid.MVVM", DiagnosticSeverity.Error, true);

    private static (ViewData? Data, ImmutableArray<Diagnostic> Diagnostics) FindView(
        GeneratorAttributeSyntaxContext context,
        CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol) return (null, ImmutableArray<Diagnostic>.Empty);
        var declaration = (TypeDeclarationSyntax)context.TargetNode;
        if (symbol.IsStatic || !declaration.Modifiers.Any(SyntaxKind.PartialKeyword))
            return (null, ImmutableArray.Create(Diagnostic.Create(InvalidView, declaration.Identifier.GetLocation(), symbol.Name)));

        var inheritor = symbol.HasAnyAttributeInBases(Classes.ViewAttribute)
            ? Inheritor.InheritorViewAttribute
            : Inheritor.None;
        
        Debug.Assert(context.TargetNode is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.TargetNode);
        
        var members = BinderMembersFactory.Create(symbol, context.SemanticModel, out var diagnostics);
        if (diagnostics.Any(diagnostic => diagnostic.Severity is DiagnosticSeverity.Error)) return (null, diagnostics);
        var inheritedDeclaredIds = BinderMembersFactory.CollectInheritedIds(symbol, candidate, context.SemanticModel);

        return (new ViewData(symbol, context.SemanticModel.Compilation, inheritor, candidate, members, GetGenericViews(symbol), inheritedDeclaredIds), diagnostics);
    }

    private static ImmutableArray<GenericViewData> GetGenericViews(INamedTypeSymbol symbol)
    {
        var genericViews = new HashSet<GenericViewData>();
        
        for (var type = symbol; type is not null; type = type.BaseType)
        {
            foreach (var @interface in type.Interfaces)
            {
                if (!@interface.IsGenericType) continue;
                if (@interface.Name != Classes.IView.Name) continue;
            
                var isSelf = SymbolEqualityComparer.Default.Equals(type, symbol);
                var data = new GenericViewData(isSelf, @interface.TypeArguments[0]);

                genericViews.Remove(data);
                genericViews.Add(data);
            }
        }
        
        return genericViews.ToImmutableArray();
    }
}