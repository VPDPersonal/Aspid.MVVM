using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Members;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

public static class BindableCommandFactory
{
    private const string CanExecuteReturnType = "bool";
    
    public static IReadOnlyCollection<BindableCommand> Create(
        ImmutableArray<IMethodSymbol> methods,
        ImmutableArray<IPropertySymbol> properties,
        ImmutableArray<string> generatedBoolProperties)
    {
        var bindableCommands = new List<BindableCommand>();
        
        var boolMethods = methods.Where(boolMethods =>
            boolMethods.ReturnType.ToString() is CanExecuteReturnType).ToImmutableArray();
        
        var boolProperties = properties.Where(property => 
            property.Type.ToString() == CanExecuteReturnType).ToImmutableArray();
        
        foreach (var method in methods)
        {
            if (!method.TryGetAnyAttributeInSelf(out var attribute, Classes.RelayCommandAttribute)) continue;
            
            var canExecuteArgument = attribute!.NamedArguments
                .Where(pair => pair.Key == "CanExecute")
                .Select(pair => pair.Value.Value as string)
                .FirstOrDefault();

            var canExecute = GetCanExecute(canExecuteArgument, method, boolMethods, boolProperties, generatedBoolProperties);

            bindableCommands.Add(canExecute.isEixst ?
                new BindableCommand(method, canExecuteArgument, canExecute.isLamda, canExecute.isMethod) :
                new BindableCommand(method, null, false, false));
        }

        return bindableCommands;
    }

    private static (bool isEixst, bool isLamda, bool isMethod) GetCanExecute(
        string? canExecuteArgument,
        IMethodSymbol method,
        ImmutableArray<IMethodSymbol> boolMethods,
        ImmutableArray<IPropertySymbol> boolProperties,
        ImmutableArray<string> generatedBoolProperties)
    {
        if (string.IsNullOrWhiteSpace(canExecuteArgument)) return (false, false, false);
        
        var isLambda = false;
        var isMethod = false;
        var isCanExecuteExist = false;
                
        var canExecuteMethodCandidates = boolMethods.Where(boolMethod =>
            boolMethod.Name == canExecuteArgument).ToArray();

        if (canExecuteMethodCandidates.Length > 0)
        {
            isCanExecuteExist = canExecuteMethodCandidates.Any(candidate => candidate.Parameters.Length == method.Parameters.Length);
            isLambda = !isCanExecuteExist && canExecuteMethodCandidates.Any(candidate => candidate.Parameters.Length == 0);

            if (isLambda) isCanExecuteExist = true;
            if (isCanExecuteExist) isMethod = true;
        }

        if (!isCanExecuteExist)
        {
            var canExecuteProperties = boolProperties.Where(property => 
                property.Name == canExecuteArgument).ToArray();
                    
            isCanExecuteExist = canExecuteProperties.Any();
            isLambda = isCanExecuteExist;
        }
                
        if (!isCanExecuteExist)
        {
            var canExecuteProperties = generatedBoolProperties.Where(property => property == canExecuteArgument);
            if (canExecuteProperties.Any()) isCanExecuteExist = true;
            isLambda = isCanExecuteExist;
        }
            
        return (isCanExecuteExist, isLambda, isMethod);
    }
}