using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

public static class BindableCommandFactory
{
    public static IReadOnlyCollection<BindableCommandInfo> Create(
        ImmutableArray<IMethodSymbol> methods,
        Compilation compilation)
    {
        var commands = new List<BindableCommandInfo>();
        foreach (var method in methods)
        {
            if (!method.TryGetAnyAttributeInSelf(out var attribute, Classes.RelayCommandAttribute)) continue;

            if (!method.ReturnsVoid || method.IsGenericMethod || method.Parameters.Length > 4
                || method.Parameters.Any(parameter => parameter.RefKind is not RefKind.None)) continue;

            var name = attribute!.NamedArguments.FirstOrDefault(argument => argument.Key == "CanExecute").Value.Value as string;
            var isLambda = false;
            var isMethod = false;
            if (name is not null && !CommandPredicateResolver.TryResolve(method.ContainingType, method, name,
                compilation, out isLambda, out isMethod))
                continue;

            commands.Add(new BindableCommandInfo(method, name, isLambda, isMethod));
        }

        return commands;
    }
}
