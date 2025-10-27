using System.Threading;
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
    private static ViewData? FindView(
        GeneratorAttributeSyntaxContext context,
        CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol) return null;

        var inheritor = symbol.HasAnyAttributeInBases(Classes.ViewAttribute)
            ? Inheritor.InheritorViewAttribute
            : Inheritor.None;
        
        var members = BinderMembersFactory.Create(symbol, context.SemanticModel);

        Debug.Assert(context.TargetNode is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.TargetNode);

        return new ViewData(symbol, inheritor, candidate, members, GetGenericViews(symbol));
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