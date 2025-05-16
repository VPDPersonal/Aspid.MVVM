using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using MVVMGenerators.Helpers.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Body;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Declarations;
using MVVMGenerators.Generators.ViewModels.Factories;
using MVVMGenerators.Generators.ViewModels.Data.Members.Collections;

namespace MVVMGenerators.Generators.ViewModels;

[Generator(LanguageNames.CSharp)]
public sealed class ViewModelGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.ForAttributeWithMetadataName(Classes.ViewModelAttribute.FullName, SyntacticPredicate, FindViewModels)
            .Where(static foundForSourceGenerator => foundForSourceGenerator.IsNeed)
            .Select(static (foundForSourceGenerator, _) => foundForSourceGenerator.Container);

        context.RegisterSourceOutput(
            source: provider,
            action: GenerateCode);
    }

    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        // TODO add support for static
        return node is ClassDeclarationSyntax { AttributeLists.Count: > 0 } candidate
            && candidate.Modifiers.Any(SyntaxKind.PartialKeyword)
            && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword);
    }
    
    private static FoundForGenerator<ViewModelData> FindViewModels(GeneratorAttributeSyntaxContext context,
        CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol) return default;
        
        Debug.Assert(context.TargetNode is ClassDeclarationSyntax);
        var candidate = Unsafe.As<ClassDeclarationSyntax>(context.TargetNode);
        
        var inheritor = symbol.HasAttributeInBases(Classes.ViewModelAttribute) 
            ? Inheritor.Inheritor
            : Inheritor.None;

        var bindableMembers = BindableMembersFactory.Create(symbol);
        var memberByGroups = IdLengthMemberGroup.Create(bindableMembers);
        var customViewModelInterfaces = CustomViewModelInterfacesFactory.Create(symbol);
        
        var data = new ViewModelData(inheritor, symbol, candidate, bindableMembers, memberByGroups, customViewModelInterfaces);
        return new FoundForGenerator<ViewModelData>(data);
    }
    
    private static void GenerateCode(SourceProductionContext context, ViewModelData data)
    {
        var declaration = data.Declaration;
        var @namespace = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();

        PropertiesBody.Generate(@namespace, data, declarationText, context);
        RelayCommandBody.Generate(@namespace, data, declarationText, context);
        BindableMembersBody.Generate(@namespace, data, declarationText, context);
        FindBindableMembersBody.Generate(@namespace, data, declarationText, context);
    }
}