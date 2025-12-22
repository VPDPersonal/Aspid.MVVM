using System.Threading;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.Views.Factories;
using Aspid.MVVM.Generators.Generators.ViewModels.Factories;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.Ids;

public partial class IdGenerator
{
    private static HashSet<string>? GetIdsForSourceGeneration(
        GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        var syntax = (TypeDeclarationSyntax)context.Node;
        if (context.SemanticModel.GetDeclaredSymbol(syntax) is not { } symbol) return null;

        var ids = new HashSet<string>();

        if (symbol.TryGetAnyAttributeInSelf(out var attribute, Classes.ViewAttribute, Classes.ViewModelAttribute))
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
                var members = BindableMembersFactory.Create(symbol, syntax, out _);

                foreach (var member in members)
                    ids.Add(member.Id.SourceValue);
            }
        }

        return ids.Count is 0 ? null : ids;
    }
}