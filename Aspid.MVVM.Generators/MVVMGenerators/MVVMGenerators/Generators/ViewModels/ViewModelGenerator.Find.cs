using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Runtime.CompilerServices;
using MVVMGenerators.Helpers.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Factories;
using MVVMGenerators.Generators.ViewModels.Data.Members.Collections;

namespace MVVMGenerators.Generators.ViewModels;

public partial class ViewModelGenerator
{
    private static FoundForGenerator<ViewModelData> FindViewModels(GeneratorAttributeSyntaxContext context,
        CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol) return default;
        
        var inheritor = RecognizeInheritor(symbol);
        var bindableMembers = BindableMembersFactory.Create(symbol);
        
        Debug.Assert(context.TargetNode is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.TargetNode);

        var groups = GroupsFactory.Create(bindableMembers);
        
        return new FoundForGenerator<ViewModelData>(true,
            new ViewModelData(inheritor, symbol, candidate, bindableMembers, groups.hashCodeGroup, groups.idLengthGroups));
    }
    
    private static Inheritor RecognizeInheritor(INamedTypeSymbol symbol)
    {
        var baseType = symbol.BaseType;
        
        // Strictly defined order
        for (var type = baseType; type is not null; type = type.BaseType)
        {
            if (type.HasAttribute(Classes.ViewModelAttribute)) 
                return Inheritor.InheritorViewModelAttribute;
        }
        
        return Inheritor.None;
    }
}