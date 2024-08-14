using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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
        
        var fieldData = FindFields(fields);

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

    private static ImmutableArray<FieldData> FindFields(IReadOnlyCollection<IFieldSymbol> fields)
    {
        var data = new List<FieldData>();
        
        foreach (var field in fields)
        {
            var isBind = field.HasAttribute(Classes.BindAttribute);
            var idReadOnlyBind = !isBind && field.HasAttribute(Classes.ReadOnlyBindAttribute);
            
            if (!isBind && !idReadOnlyBind) continue;

            var getAccess = -1;
            var setAccess = -1;

            if (field.HasAttribute(Classes.AccessAttribute, out var accessAttribute))
            {
                if (accessAttribute!.ConstructorArguments.Length > 0)
                {
                    var value = (int)(accessAttribute!.ConstructorArguments[0].Value ?? 0);
                    getAccess = value;
                    setAccess = value;
                }

                foreach (var argument in accessAttribute!.NamedArguments)
                {
                    switch (argument.Key)
                    {
                        case "Get":
                            getAccess = (int)(argument.Value.Value ?? -1);
                            break;
                        case "Set":
                            setAccess = (int)(argument.Value.Value ?? -1);
                            break;
                    }
                }
            }

            if (idReadOnlyBind)
                setAccess = getAccess;

            data.Add(new FieldData(field, getAccess, setAccess, idReadOnlyBind));
        }

        return ImmutableArray.CreateRange(data);
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