using System.Threading;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using MVVMGenerators.Helpers.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.Views.Factories;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Factories;

namespace MVVMGenerators.Generators.Ids;

public partial class IdGenerator
{
    private static FoundForGenerator<HashSet<string>> GetIdsForSourceGeneration(
        GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        var syntax = (TypeDeclarationSyntax)context.Node;
        if (context.SemanticModel.GetDeclaredSymbol(syntax) is not { } symbol) return default;

        var ids = new HashSet<string>();

        if (symbol.HasAttribute(Classes.ViewAttribute))
        {
            var members = BinderMembersFactory.Create(symbol, context.SemanticModel);
            
            foreach (var member in members)
                ids.Add(member.Id.SourceValue);
        }
        
        if (symbol.HasAttribute(Classes.ViewModelAttribute))
        {
            var members = BindableMembersFactory.Create(symbol);

            foreach (var member in members)
                ids.Add(member.Id.SourceValue);
        }

        return ids.Count is 0 
            ? default 
            : new FoundForGenerator<HashSet<string>>(ids);
    }
}