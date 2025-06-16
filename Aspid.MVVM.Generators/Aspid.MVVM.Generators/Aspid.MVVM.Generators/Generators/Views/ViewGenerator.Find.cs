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