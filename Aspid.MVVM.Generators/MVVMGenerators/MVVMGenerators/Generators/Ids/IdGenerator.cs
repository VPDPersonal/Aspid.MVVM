using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using MVVMGenerators.Generators.Views;
using MVVMGenerators.Helpers.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.ViewModels;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.Ids;

[Generator(LanguageNames.CSharp)]
public class IdGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(SyntacticPredicate, GetIdsForSourceGeneration)
            .Where(foundFor => foundFor.IsNeed)
            .Select((foundFor, _) => foundFor.Container);
        
        context.RegisterSourceOutput(provider.Collect(), GenerateCode);
    }

    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        var candidate = node switch
        {
            ClassDeclarationSyntax or StructDeclarationSyntax => node as TypeDeclarationSyntax,
            _ => null
        };

        return candidate is not null
            && candidate.AttributeLists.Count > 0
            && candidate.Modifiers.Any(SyntaxKind.PartialKeyword)
            && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword);
    }
    
    private static FoundForGenerator<HashSet<string>> GetIdsForSourceGeneration(
        GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        var syntax = (TypeDeclarationSyntax)context.Node;
        if (context.SemanticModel.GetDeclaredSymbol(syntax) is not { } symbol) return default;

        var ids = new HashSet<string>();

        if (symbol.HasAttribute(Classes.ViewAttribute))
        {
            var members = ViewGenerator.GetViewMembers(symbol.GetMembers(), context.SemanticModel);
            
            foreach (var field in members.Fields)
            {
                var id = field.Field.GetId();
                ids.Add(id);
            }
        
            foreach (var property in members.Properties)
            {
                var id = property.Property.GetId();
                ids.Add(id);
            }
        
            foreach (var member in members.AsBinderMembers)
            {
                var id = member.Member.GetId();
                ids.Add(id);
            }
        }
        
        if (symbol.HasAttribute(Classes.ViewModelAttribute))
        {
            var fields = new List<IFieldSymbol>();
            var methods = new List<IMethodSymbol>();
            var properties = new List<IPropertySymbol>();
            symbol.FillMembers(fields, methods, properties);
        
            var fieldData = ViewModelGenerator.FindFields(fields, properties);
            var bindAlsoProperties = new HashSet<BindAlsoProperty>();

            foreach (var property in fieldData.SelectMany(field => field.BindAlso))
            {
                bindAlsoProperties.Add(property);
            }

            var generatedProperties = fieldData
                .Where(field => field.Type.ToString() == "bool")
                .Select(field => FieldSymbolExtensions.GetPropertyName(field.FieldName))
                .ToArray();
        
            var commandData = ViewModelGenerator.FindCommand(methods, properties, generatedProperties);
            
            foreach (var field in fieldData)
            {
                var id = field.Field.GetId();
                ids.Add(id);
            }
            
            foreach (var command in commandData)
            {
                var id = command.Execute.GetId("Command");
                ids.Add(id);
            }
            
            foreach (var property in bindAlsoProperties)
            {
                var id = property.Property.GetId();
                ids.Add(id);
            }
        }

        return ids.Count == 0 
            ? default 
            : new FoundForGenerator<HashSet<string>>(true, ids);
    }

    private static void GenerateCode(SourceProductionContext context, ImmutableArray<HashSet<string>> idsList)
    {
        var code = new CodeWriter();

        code.AppendLine("namespace Aspid.MVVM.Generated")
            .BeginBlock()
            .AppendLine("internal static class Ids")
            .BeginBlock();
        
        var allIds = new HashSet<string>();
        
        foreach (var ids in idsList)
        {
            foreach (var id in ids)
            {
                allIds.Add(id);
            }
        }

        foreach (var id in allIds)
        {
            code.AppendLine($"public const string {id} = \"{id}\";");
        }

        code.EndBlock();
        code.EndBlock();

        context.AddSource("Ids", code.GetSourceText());
    }
}