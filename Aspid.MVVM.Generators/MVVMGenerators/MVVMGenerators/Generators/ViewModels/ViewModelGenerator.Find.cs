using System.Linq;
using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using MVVMGenerators.Helpers.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Factories;
using MVVMGenerators.Generators.ViewModels.Data.Members;
using MVVMGenerators.Generators.ViewModels.Data.Members.Fields;

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
        var bindAlsoProperties = new HashSet<BindAlsoProperty>();

        foreach (var property in fieldData.SelectMany(field => field.BindAlso))
        {
            bindAlsoProperties.Add(property);
        }

        var generatedProperties = fieldData
            .Where(field => field.Type.ToString() == "bool")
            .Select(field => FieldSymbolExtensions.GetPropertyName(field.FieldName))
            .ToArray();
        
        var commandData = FindCommand(methods, properties, generatedProperties);

        Debug.Assert(context.TargetNode is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.TargetNode);
        
        return new FoundForGenerator<ViewModelData>(true,
            new ViewModelData(inheritor, candidate, fieldData, commandData, bindAlsoProperties));
    }
    
    private static Inheritor RecognizeInheritor(INamedTypeSymbol symbol)
    {
        var baseType = symbol.BaseType;
        
        // Strictly defined order
        for (var type = baseType; type is not null; type = type.BaseType)
        {
            if (type.HasAttribute(Classes.ViewModelAttribute)) 
                return Inheritor.InheritorViewModelAttribute;
        }
        
        if (symbol.HasBaseType(Classes.ViewModel, Classes.MonoViewModel, Classes.ScriptableViewModel)) return Inheritor.InheritorViewModel;
        if (baseType is not null && baseType.HasInterface(Classes.IViewModel)) return Inheritor.HasInterface;

        return symbol.HaseDirectInterface(Classes.IViewModel) ? Inheritor.HasInterface : Inheritor.None;
    }
    
    private static ViewModelFields FindFields(
        IReadOnlyCollection<IFieldSymbol> fields,
        IReadOnlyCollection<IPropertySymbol> properties)
    {
        var viewModelFields = new List<ViewModelField>();
        var fieldFactory = new ViewModelFieldFactory(properties);

        foreach (var field in fields)
        {
            if (fieldFactory.TryCreate(field, out var viewModelField))
                viewModelFields.Add(viewModelField);
        }

        return new ViewModelFields(viewModelFields);
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