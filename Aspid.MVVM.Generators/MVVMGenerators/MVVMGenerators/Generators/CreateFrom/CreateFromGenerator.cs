using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
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
            ConstructorDeclarationSyntax syntax => syntax,
            _ => null
        };

        return candidate is not null && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword);
    }

    private static FoundForGenerator<CreateFromData> FindCreateFrom(
        GeneratorAttributeSyntaxContext context,
        CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not IMethodSymbol constructor) return default;
        
        var attribute = context.Attributes.First(attribute =>
        attribute.AttributeClass?.ToDisplayString() == Classes.CreateFromAttribute.FullName);
        
        if (attribute.ConstructorArguments.First().Value is not ITypeSymbol fromType) return default;
        
        if (constructor.Parameters.Length == 0) return default;
        var candidate = Unsafe.As<ConstructorDeclarationSyntax>(context.TargetNode);
        return new FoundForGenerator<CreateFromData>(true, new CreateFromData(candidate, constructor, fromType));
    }

    private static void GenerateCode(SourceProductionContext context, CreateFromData data)
    {
        var @namespace = data.Declaration.GetNamespaceName();
        var dataSpan = new CreateFromDataSpan(data);

        if (data.Declaration.Parent is not TypeDeclarationSyntax typeDeclaration) return;

        var i = 0;
        var index = -1;

        foreach (var constructor in typeDeclaration.Members.OfType<ConstructorDeclarationSyntax>())
        {
            if (constructor == data.Declaration)
            {
                index = i;
                break;
            }

            i++;
        }
        
        if (index == -1) return;
        
        var declaration = new DeclarationText(
            "public static", 
            "class", 
            $"{dataSpan.FromType.ToDisplayString().Replace(".", "_")}To{dataSpan.ToType.ToDisplayString().Replace(".", "_")}_{index}",
            null);
        
        var code = new CodeWriter();
        code.AppendClassBegin(@namespace, declaration)
            .AppendCreateFromBody(dataSpan)
            .AppendClassEnd(@namespace);
        
        context.AddSource(declaration.GetFileName(@namespace, "IViewModel"), code.GetSourceText());
    }
}