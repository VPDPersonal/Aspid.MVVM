using System.Linq;
using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using MVVMGenerators.Helpers.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Helpers.Extensions.Declarations;

namespace MVVMGenerators.Generators.ViewModels;

public partial class ViewModelGenerator
{
    private static FoundForGenerator<ViewModelData> FindViewModels(GeneratorSyntaxContext context,
        CancellationToken cancellationToken)
    {
        Debug.Assert(context.Node is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.Node);
        if (!candidate.HasAttribute(context.SemanticModel, Classes.ViewModelAttribute.FullName)) return default;

        var symbol = context.SemanticModel.GetDeclaredSymbol(candidate, cancellationToken);
        if (symbol is null) return default;

        var fields = new List<IFieldSymbol>();
        var methods = new List<IMethodSymbol>();
        var properties = new List<IPropertySymbol>();
        
        symbol.FillMembers(fields: fields, methods: methods, properties: properties);
        
        var fieldData = FindFields(fields, properties);

        var generatedProperties = fieldData
            .Where(field => field.Type.ToString() == "bool")
            .Select(field => FieldSymbolExtensions.GetPropertyName(field.Name))
            .ToArray();
        
        var commandData = FindCommand(methods, properties, generatedProperties);
        
        var hasBaseType = false;
        var hasInterface = symbol.HasInterface(Classes.IViewModel.FullName);

        for (var type = symbol.BaseType; type != null; type = type.BaseType)
        {
            if (type.HasInterface(Classes.IViewModel.FullName))
                hasInterface = true;

            if (type.HasAttribute(Classes.ViewModelAttribute.FullName))
                hasBaseType = true;

            if (hasInterface && hasBaseType) break;
        }

        return new FoundForGenerator<ViewModelData>(true,
            new ViewModelData(hasBaseType, hasInterface, candidate, fieldData, commandData));
    }

    private static ImmutableArray<FieldData> FindFields(
        IReadOnlyCollection<IFieldSymbol> fields,
        IReadOnlyCollection<IPropertySymbol> properties)
    {
        var data = new List<FieldData>();
        
        foreach (var field in fields)
        {
            var hasReadOnlyBindAttribute = false;
            var hasBindAttribute = field.HasAttribute(Classes.BindAttribute);
            
            if (!hasBindAttribute)
            {
                hasReadOnlyBindAttribute = field.HasAttribute(Classes.ReadOnlyBindAttribute);
                if (!hasReadOnlyBindAttribute) continue;
            }
            
            var accessors = GetAccessors(field);
            var bindAlso = GetBindAlso(field);
            
            if (hasReadOnlyBindAttribute) accessors.Set = accessors.Get;
            data.Add(new FieldData(field, hasReadOnlyBindAttribute, accessors.Get, accessors.Set, bindAlso));
        }

        return ImmutableArray.CreateRange(data);

        IEnumerable<IPropertySymbol> GetBindAlso(IFieldSymbol field)
        {
            var bindAlso = new List<IPropertySymbol>();
            var attributesArgument = new List<string?>();
            
            foreach (var attribute in field.GetAttributes())
            {
                if (attribute.ConstructorArguments.Length != 1) continue;
                if (attribute.AttributeClass?.ToDisplayString() != Classes.BindAlsoAttribute.FullName) continue;
                attributesArgument.Add(attribute.ConstructorArguments[0].Value?.ToString());
            }

            foreach (var property in properties)
            {
                if (attributesArgument.Any(argument => property.Name == argument))
                    bindAlso.Add(property);
            }
            
            return bindAlso;
        }
        
        (SyntaxKind Get, SyntaxKind Set) GetAccessors(IFieldSymbol field)
        {
            (SyntaxKind Get, SyntaxKind Set) accessors = (SyntaxKind.PrivateKeyword, SyntaxKind.PrivateKeyword);
            if (!field.HasAttribute(Classes.AccessAttribute, out var accessAttribute)) return accessors;
            
            if (accessAttribute!.ConstructorArguments.Length == 1)
            {
                var value = (SyntaxKind)(int)(accessAttribute.ConstructorArguments[0].Value ?? SyntaxKind.PrivateKeyword);
                accessors.Get = value;
                accessors.Set = value;
            }
            
            foreach (var argument in accessAttribute!.NamedArguments)
            {
                switch (argument.Key)
                {
                    case "Get": accessors.Get = (SyntaxKind)(int)(argument.Value.Value ?? SyntaxKind.PrivateKeyword); break;
                    case "Set": accessors.Set = (SyntaxKind)(int)(argument.Value.Value ?? SyntaxKind.PrivateKeyword); break;
                }
            }

            return accessors;
        }
    }

    private static ImmutableArray<RelayCommandData> FindCommand(
        IReadOnlyCollection<IMethodSymbol> methods,
        IReadOnlyCollection<IPropertySymbol> properties,
        IReadOnlyCollection<string> generatedBoolProperties)
    {
        var commandData = new List<RelayCommandData>();

        foreach (var method in methods)
        {
            if (!method.HasAttribute(Classes.RelayCommandAttribute, out var attribute)) continue;
            
            var canExecuteArgument = attribute!.NamedArguments
                .Where(pair => pair.Key == "CanExecute")
                .Select(pair => pair.Value.Value as string)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(canExecuteArgument))
            {
                var isExist = false;
                var isLambda = false;
                var isMethod = false;

                var canExecuteMethods = methods.Where(method2 =>
                    method2.ReturnType.ToString() == "bool" &&
                    method2.Name == canExecuteArgument).ToArray();
                
                if (canExecuteMethods.Length > 0)
                {
                    isExist = canExecuteMethods.Any(method2 => method2.Parameters.Length == method.Parameters.Length);
                    isLambda = !isExist && canExecuteMethods.Any(method2 => method2.Parameters.Length == 0);
                    
                    if (isLambda) isExist = true;
                    if (isExist) isMethod = true;
                }
                
                if (!isExist)
                {
                    var canExecuteProperties = properties.Where(property => 
                        property.Type.ToString() == "bool" &&
                        property.Name == canExecuteArgument).ToArray();
                    
                    isExist = canExecuteProperties.Any();
                    isLambda = isExist;
                }

                if (!isExist)
                {
                    var canExecuteProperties = generatedBoolProperties.Where(property => property == canExecuteArgument);
                    if (canExecuteProperties.Any()) isExist = true;
                    isLambda = isExist;
                }

                if (!isExist) continue;
                commandData.Add(new RelayCommandData(method, canExecuteArgument, isMethod, isLambda));
            }
            else
            {
                commandData.Add(new RelayCommandData(method));
            }
        }
        
        return ImmutableArray.CreateRange(commandData);
    }
}