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

        symbol.FillMembers(fields: fields, methods: methods);
        
        var fieldData = FindFields(fields);
        var commandData = FindCommand(methods);
        
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
            if (!field.HasAttribute(Classes.BindAttribute.FullName)) continue;

            var getAccess = -1;
            var setAccess = -1;

            if (field.HasAttribute(Classes.AccessAttribute.FullName, out var accessAttribute))
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

            data.Add(new FieldData(field, getAccess, setAccess));
        }

        return ImmutableArray.CreateRange(data);
    }

    private static ImmutableArray<RelayCommandData> FindCommand(IReadOnlyCollection<IMethodSymbol> methods)
    {
        var commandData = new List<RelayCommandData>();
        
        foreach (var executeMethod in methods)
        {
            if (!executeMethod.HasAttribute(Classes.RelayCommand, out var attribute)) continue;
            
            TypedConstant? argument = null;
            foreach (var namedArgument in attribute!.NamedArguments)
            {
                if (namedArgument.Key != "CanExecute") continue;

                argument = namedArgument.Value;
                break;
            }

            if (argument is not null)
            {
                if (argument.Value.Value is null) continue;
                var canExecuteName = (string)argument.Value.Value;
                
                if (TryGetCanExecuteMethod(canExecuteName, executeMethod.TypeArguments, out var canExecuteMethod))
                    commandData.Add(new RelayCommandData(executeMethod, canExecuteMethod));
                
                continue; 
            }

            commandData.Add(new RelayCommandData(executeMethod));
        }

        return ImmutableArray.CreateRange(commandData);

        bool TryGetCanExecuteMethod(
            string canExecuteName,
            ImmutableArray<ITypeSymbol> canExecuteArguments, 
            out IMethodSymbol? canExecuteMethod)
        {
            canExecuteMethod = null;
                    
            foreach (var method in methods)
            {
                if (method.Name != canExecuteName) continue;

                var arguments = method.TypeArguments;
                if (canExecuteArguments.Length != arguments.Length) continue;

                var argumentsAreEqual = true;
                for (var i = 0; i < arguments.Length; i++)
                {
                    var canExecuteTypeArgument = canExecuteArguments[i].ContainingType?.ToDisplayString();
                    var typeArgument = arguments[i].ContainingType?.ToDisplayString();
                    if (typeArgument == canExecuteTypeArgument) continue;

                    argumentsAreEqual = false;
                    break;
                }
                        
                if (!argumentsAreEqual) continue;
                
                canExecuteMethod = method;
                return true;
            }

            return false;
        }
    }
}