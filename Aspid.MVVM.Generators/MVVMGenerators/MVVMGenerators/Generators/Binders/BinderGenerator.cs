using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVVMGenerators.Generators.Binders;

[Generator]
public partial class BinderGenerator : IIncrementalGenerator
{
    private const string PartialBinderLogName = "BinderLog";
    
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(SyntacticPredicate, FindBinders)
            .Where(foundForSourceGenerator => foundForSourceGenerator.IsNeed)
            .Select((foundForSourceGenerator, _) => foundForSourceGenerator.Container);
        
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

        // Must implement IBinder or inherit from a type that inherits IBinder
        return candidate is not null
            && candidate.Modifiers.Any(SyntaxKind.PartialKeyword)
            && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword)
            && candidate.BaseList is { Types.Count: > 0 };
    }
}