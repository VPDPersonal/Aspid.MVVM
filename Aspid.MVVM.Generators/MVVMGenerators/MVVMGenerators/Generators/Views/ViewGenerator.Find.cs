using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Runtime.CompilerServices;
using MVVMGenerators.Helpers.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.Views.Factories;

namespace MVVMGenerators.Generators.Views;

public partial class ViewGenerator
{
    private static FoundForGenerator<ViewData> FindView(
        GeneratorAttributeSyntaxContext context,
        CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol) return default;

        var inheritor = RecognizeInheritor(symbol);
        var members = BinderMembersFactory.Create(symbol, context.SemanticModel);

        Debug.Assert(context.TargetNode is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.TargetNode);

        var viewData = new ViewData(inheritor, candidate, members);
        return new FoundForGenerator<ViewData>(true, viewData);
    }

    private static Inheritor RecognizeInheritor(INamedTypeSymbol symbol)
    {
        var baseType = symbol.BaseType;
        
        // Strictly defined order
        for (var type = baseType; type is not null; type = type.BaseType)
        {
            if (type.HasAttribute(Classes.ViewAttribute)) 
                return Inheritor.InheritorViewAttribute;
        }

        return Inheritor.None;
    }
}