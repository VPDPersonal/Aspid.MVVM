using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Aspid.MVVM.Generators.Views.Data;
using Aspid.MVVM.Generators.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Views.Factories;

namespace Aspid.MVVM.Generators.Views;

public partial class ViewGenerator
{
    private static FoundForGenerator<ViewData> FindView(
        GeneratorAttributeSyntaxContext context,
        CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol) return default;

        var inheritor = symbol.HasAttributeInBases(Classes.ViewAttribute)
            ? Inheritor.InheritorViewAttribute
            : Inheritor.None;
        
        var members = BinderMembersFactory.Create(symbol, context.SemanticModel);

        Debug.Assert(context.TargetNode is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.TargetNode);

        var viewData = new ViewData(symbol, inheritor, candidate, members, GetGenericViews(symbol));
        return new FoundForGenerator<ViewData>(viewData);
    }

    private static ImmutableArray<ITypeSymbol> GetGenericViews(INamedTypeSymbol symbol)
    {
        var genericViews = new List<ITypeSymbol>();
        
        foreach (var @interface in symbol.Interfaces)
        {
            if (@interface.IsGenericType)
            {
                if (@interface.Name == Classes.IView.Name)
                {
                    genericViews.Add(@interface.TypeArguments[0]);
                }
            }
        }

        return genericViews.ToImmutableArray();
    }
}