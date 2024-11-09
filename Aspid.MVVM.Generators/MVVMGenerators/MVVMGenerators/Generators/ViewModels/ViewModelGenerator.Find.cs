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
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels;

public partial class ViewModelGenerator
{
    private static FoundForGenerator<ViewModelData> FindViewModels(GeneratorAttributeSyntaxContext context,
        CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol) return default;

        var fields = new List<IFieldSymbol>();
        var methods = new List<IMethodSymbol>();
        var properties = new List<IPropertySymbol>();
        
        var inheritor = RecognizeInheritor(symbol);
        symbol.FillMembers(fields, methods, properties);
        
        var fieldData = FindFields(fields, properties);

        var generatedProperties = fieldData
            .Where(field => field.Type.ToString() == "bool")
            .Select(field => FieldSymbolExtensions.GetPropertyName(field.Name))
            .ToArray();
        
        var commandData = FindCommand(methods, properties, generatedProperties);

        Debug.Assert(context.TargetNode is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.TargetNode);
        
        return new FoundForGenerator<ViewModelData>(true,
            new ViewModelData(inheritor, candidate, fieldData, commandData));
    }
    
    private static Inheritor RecognizeInheritor(INamedTypeSymbol symbol)
    {
        // Strictly defined order
        for (var type = symbol.BaseType; type is not null; type = type.BaseType)
        {
            if (type.HasAttribute(Classes.ViewModelAttribute)) 
                return Inheritor.InheritorViewModelAttribute;
        }
        
        if (symbol.HasBaseType(Classes.ViewModel, Classes.MonoViewModel)) return Inheritor.InheritorViewModel;
        if (symbol.HasInterface(Classes.IViewModel)) return Inheritor.HasInterface;

        return Inheritor.None;
    }

    private static ImmutableArray<FieldInViewModel> FindFields(
        IReadOnlyCollection<IFieldSymbol> fields,
        IReadOnlyCollection<IPropertySymbol> properties)
    {
        var data = new List<FieldInViewModel>();
        
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
            data.Add(new FieldInViewModel(field, hasReadOnlyBindAttribute, accessors.Get, accessors.Set, bindAlso));
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