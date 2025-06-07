using System.Threading;
using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Views.Factories;
using Aspid.MVVM.Generators.ViewModels.Factories;

namespace Aspid.MVVM.Generators.Ids;

public partial class IdGenerator
{
    private static FoundForGenerator<HashSet<string>> GetIdsForSourceGeneration(
        GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        var syntax = (TypeDeclarationSyntax)context.Node;
        if (context.SemanticModel.GetDeclaredSymbol(syntax) is not { } symbol) return default;

        var ids = new HashSet<string>();

        if (symbol.HasAnyAttribute(out var attribute, Classes.ViewAttribute, Classes.ViewModelAttribute))
        {
            var attributeName = attribute!.AttributeClass!.ToDisplayString();

            if (attributeName == Classes.ViewAttribute.FullName)
            {
                var members = BinderMembersFactory.Create(symbol, context.SemanticModel);
            
                foreach (var member in members)
                    ids.Add(member.Id.SourceValue);
            }
            else
            {
                var members = BindableMembersFactory.Create(symbol);

                foreach (var member in members)
                    ids.Add(member.Id.SourceValue);
            }
        }

        return ids.Count is 0 
            ? default 
            : new FoundForGenerator<HashSet<string>>(ids);
    }
}