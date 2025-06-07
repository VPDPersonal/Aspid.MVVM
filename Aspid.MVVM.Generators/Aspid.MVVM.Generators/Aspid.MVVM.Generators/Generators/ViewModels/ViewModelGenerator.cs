using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.ViewModels.Body;
using Aspid.MVVM.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.ViewModels.Factories;
using Aspid.MVVM.Generators.ViewModels.Data.Members.Collections;
using Unsafe = System.Runtime.CompilerServices.Unsafe;
using static Aspid.MVVM.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.ViewModels;

[Generator(LanguageNames.CSharp)]
public sealed class ViewModelGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.ForAttributeWithMetadataName(ViewModelAttribute.FullName, SyntacticPredicate, FindViewModels)
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
        
        var inheritor = symbol.HasAttributeInBases(ViewModelAttribute) 
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