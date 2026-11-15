using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Generators.ViewModels.Extensions;
using BindMode = Aspid.MVVM.Generators.Generators.ViewModels.Data.BindMode;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

internal static class CommandPredicateResolver
{
    public static bool TryResolve(
        INamedTypeSymbol viewModel,
        IMethodSymbol command,
        string name,
        Compilation compilation,
        out bool isLambda,
        out bool isMethod)
    {
        isLambda = false;
        isMethod = false;
        if (string.IsNullOrWhiteSpace(name)) return false;

        var members = AccessibleMembers(viewModel, compilation).Where(member => member.Name == name).ToArray();
        var methods = members.OfType<IMethodSymbol>()
            .Where(method => method.MethodKind is MethodKind.Ordinary
                && method.ReturnType.SpecialType is SpecialType.System_Boolean
                && method.Parameters.All(parameter => parameter.RefKind is RefKind.None))
            .ToArray();

        if (methods.Any(method => ParametersMatch(command, method, compilation)))
        {
            isMethod = true;
            return true;
        }

        if (methods.Any(method => !method.IsGenericMethod && method.Parameters.Length is 0))
        {
            isLambda = true;
            isMethod = true;
            return true;
        }

        if (members.OfType<IPropertySymbol>().Any(property => property.Type.SpecialType is SpecialType.System_Boolean
            && !property.IsIndexer && property.GetMethod is { } getter
            && compilation.IsSymbolAccessibleWithin(getter, viewModel)))
        {
            isLambda = true;
            return true;
        }

        foreach (var field in viewModel.GetMembers().OfType<IFieldSymbol>())
        {
            if (field.Type.SpecialType is not SpecialType.System_Boolean || field.GetBindMode() is BindMode.None) continue;
            var generatedName = field.IsConst ? field.Name : field.GetPropertyName();
            if (generatedName != name) continue;

            isLambda = true;
            return true;
        }

        return false;
    }

    private static bool ParametersMatch(IMethodSymbol command, IMethodSymbol predicate, Compilation compilation)
    {
        if (predicate.Parameters.Length != command.Parameters.Length) return false;
        if (predicate.IsGenericMethod) return command.Parameters.Length > 0;

        for (var i = 0; i < command.Parameters.Length; i++)
        {
            var conversion = compilation.ClassifyCommonConversion(command.Parameters[i].Type, predicate.Parameters[i].Type);
            if (conversion.IsIdentity || conversion.IsImplicit && conversion.IsReference) continue;
            return false;
        }

        return true;
    }

    private static IEnumerable<ISymbol> AccessibleMembers(INamedTypeSymbol viewModel, Compilation compilation)
    {
        for (var type = viewModel; type is not null; type = type.BaseType)
        {
            foreach (var member in type.GetMembers())
            {
                if (compilation.IsSymbolAccessibleWithin(member, viewModel)) yield return member;
            }
        }
    }
}
