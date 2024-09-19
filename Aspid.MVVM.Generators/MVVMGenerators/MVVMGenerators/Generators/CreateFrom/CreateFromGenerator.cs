using System;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using MVVMGenerators.Helpers.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Generators.CreateFrom.Body;
using MVVMGenerators.Generators.CreateFrom.Data;
using MVVMGenerators.Helpers.Extensions.Declarations;

namespace MVVMGenerators.Generators.CreateFrom;

[Generator(LanguageNames.CSharp)]
public class CreateFromGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.ForAttributeWithMetadataName(Classes.CreateFromAttribute.FullName, SyntacticPredicate, FindCreateFrom).
            Where(foundForSourceGenerator => foundForSourceGenerator.IsNeed).
            Select((foundForSourceGenerator, _) => foundForSourceGenerator.Container);
        
        context.RegisterSourceOutput(
            source: provider,
            action: GenerateCode);
    }

    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        var candidate = node switch
        {
            ClassDeclarationSyntax or StructDeclarationSyntax => node as TypeDeclarationSyntax,
            _ => null
        };

        return candidate is not null && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword);
    }

    private static FoundForGenerator<CreateFromData> FindCreateFrom(
        GeneratorAttributeSyntaxContext context,
        CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol) return default;

        var constructors = new List<IMethodSymbol>();
        var attribute = context.Attributes.First(attribute =>
        attribute.AttributeClass?.ToDisplayString() == Classes.CreateFromAttribute.FullName);
        
        if (attribute.ConstructorArguments.First().Value is not ITypeSymbol fromType) return default;
        
        foreach (var constructor in symbol.Constructors)
        {
            if (constructor.TypeArguments.Any(type => type.ToDisplayString() != fromType.Name)) continue;
            constructors.Add(constructor);
        }
        
        if (constructors.Count == 0) return default;
        
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.TargetNode);
        return new FoundForGenerator<CreateFromData>(true, new CreateFromData(candidate, fromType, constructors.ToImmutableArray()));
    }

    private static void GenerateCode(SourceProductionContext context, CreateFromData data)
    {
        var @namespace = data.Declaration.GetNamespaceName();
        var declaration = new DeclarationText("public static", "class", $"{data.FromType.ToDisplayString().Replace(".", "_")}To{data.Declaration.Identifier.Text}", null);
        
        var code = new CodeWriter();
        code.AppendClassBegin(@namespace, declaration)
            .AppendCreateFromBody(new CreateFromDataSpan(data))
            .AppendClassEnd(@namespace);
        
        context.AddSource(declaration.GetFileName(@namespace, "IViewModel"), code.GetSourceText());
        // context.AddSource("Some", "tut");
    }
}