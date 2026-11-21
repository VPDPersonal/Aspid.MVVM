using System.Linq;
using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Shared;
using Aspid.Generators.Helper;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.ViewModels.Body;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Factories;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Members.Collections;
using Unsafe = System.Runtime.CompilerServices.Unsafe;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.ViewModels;

[Generator(LanguageNames.CSharp)]
public sealed class ViewModelGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.ForAttributeWithMetadataName(ViewModelAttribute.FullName, SyntacticPredicate, FindViewModels);

        context.RegisterSourceOutput(
            source: provider,
            action: static (production, result) =>
            {
                foreach (var diagnostic in result.Diagnostics)
                    production.ReportDiagnostic(diagnostic);

                if (result.Data is { } data) GenerateCode(production, data);
            });
    }

    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        // TODO add support for static
        return node is ClassDeclarationSyntax { AttributeLists.Count: > 0 } candidate
            && candidate.Modifiers.Any(SyntaxKind.PartialKeyword)
            && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword);
    }
    
    private static (ViewModelData? Data, ImmutableArray<Diagnostic> Diagnostics) FindViewModels(
        GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol) return (null, ImmutableArray<Diagnostic>.Empty);
        var diagnostics = ViewModelValidation.Validate(symbol, context.SemanticModel.Compilation);
        if (AspidTypeDeclaration.ValidateContainers(symbol) is { } containerDiagnostic)
            diagnostics = diagnostics.Add(containerDiagnostic);
        if (diagnostics.Any(diagnostic => diagnostic.Severity is DiagnosticSeverity.Error)) return (null, diagnostics);
        
        Debug.Assert(context.TargetNode is ClassDeclarationSyntax);
        var candidate = Unsafe.As<ClassDeclarationSyntax>(context.TargetNode);
        
        var inheritor = symbol.HasAnyAttributeInBases(ViewModelAttribute) 
            ? Inheritor.Inheritor
            : Inheritor.None;
        
        var bindableMembers = BindableMembersFactory.Create(symbol, context.SemanticModel.Compilation, out var propertyNotificationData, cancellationToken);
        diagnostics = diagnostics.AddRange(propertyNotificationData.Diagnostics);
        if (diagnostics.Any(diagnostic => diagnostic.Severity is DiagnosticSeverity.Error)) return (null, diagnostics);
        var memberByGroups = IdLengthMemberGroup.Create(bindableMembers);
        var customViewModelInterfaces = CustomViewModelInterfacesFactory.Create(symbol);
        
        return (new ViewModelData(inheritor, symbol, candidate, bindableMembers, memberByGroups, customViewModelInterfaces, propertyNotificationData), diagnostics);
    }
    
    private static void GenerateCode(SourceProductionContext context, ViewModelData data)
    {
        var declarationText = new AspidTypeDeclaration(data.Symbol);

        BindableMembers.Generate(data, declarationText, context);
        RelayCommandBody.Generate(data, declarationText, context);
        FindBindableMembersBody.Generate(data, declarationText, context);
        GeneratedPropertiesBody.Generate(data, declarationText, context);
        PropertyNotificationBody.Generate(data, declarationText, context);
        GeneratedPropertyMethodsBody.Generate(data, declarationText, context);
        BindableInterfaceMembersBody.Generate(data, declarationText, context);
    }
}