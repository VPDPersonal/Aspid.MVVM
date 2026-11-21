using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Generators.ViewModels.Extensions;
using Aspid.MVVM.Generators.Generators.ViewModels.Factories;
using BindMode = Aspid.MVVM.Generators.Generators.ViewModels.Data.BindMode;

namespace Aspid.MVVM.Generators.Generators.ViewModels;

internal static class ViewModelValidation
{
    private static readonly DiagnosticDescriptor InvalidBinding = Rule("ASPIDVM001", "Invalid binding mode", DiagnosticSeverity.Error);
    private static readonly DiagnosticDescriptor InvalidAccess = Rule("ASPIDVM002", "Invalid accessor visibility", DiagnosticSeverity.Error);
    private static readonly DiagnosticDescriptor InvalidDependency = Rule("ASPIDVM003", "Invalid binding dependency", DiagnosticSeverity.Error);
    private static readonly DiagnosticDescriptor InvalidPredicate = Rule("ASPIDVM004", "Invalid command predicate", DiagnosticSeverity.Error);
    private static readonly DiagnosticDescriptor InvalidCommand = Rule("ASPIDVM005", "Invalid relay command", DiagnosticSeverity.Error);
    private static readonly DiagnosticDescriptor SkippedBinding = Rule("ASPIDVM006", "Binding configuration is ignored", DiagnosticSeverity.Warning);

    public static ImmutableArray<Diagnostic> Validate(INamedTypeSymbol viewModel, Compilation compilation)
    {
        var diagnostics = ImmutableArray.CreateBuilder<Diagnostic>();
        var members = viewModel.GetMembers();

        foreach (var member in members)
        {
            var attributes = member.GetAttributes();
            var binds = attributes.Where(IsBind).ToArray();
            if (binds.Length > 1)
                Report(InvalidBinding, member, binds[1], "Only one bind attribute is allowed.");

            foreach (var bind in binds)
            {
                if (Is(bind, "BindAttribute") && bind.ConstructorArguments.Length > 0)
                {
                    var value = bind.ConstructorArguments[0].Value;
                    if (value is not int mode || mode is < 1 or > 4)
                    {
                        Report(InvalidBinding, member, bind, "The mode must be OneWay, TwoWay, OneTime or OneWayToSource.");
                        continue;
                    }
                }

                if (member.GetBindMode() is BindMode.None)
                    Report(SkippedBinding, member, bind, "The member cannot support the requested binding mode and will be skipped.");
            }

            foreach (var access in attributes.Where(attribute => Is(attribute, "AccessAttribute")))
            {
                if (access.ConstructorArguments.Any(argument => !IsAccess(argument.Value))
                    || access.NamedArguments.Any(argument => !IsAccess(argument.Value.Value)))
                    Report(InvalidAccess, member, access, "Accessor visibility must be Private, Protected or Public.");
            }

            foreach (var dependency in attributes.Where(attribute => Is(attribute, "BindAlsoAttribute")))
            {
                var name = dependency.ConstructorArguments.FirstOrDefault().Value as string;
                if (string.IsNullOrWhiteSpace(name))
                {
                    Report(InvalidDependency, member, dependency, "A non-empty property name is required.");
                    continue;
                }

                var target = members.FirstOrDefault(candidate => candidate is IPropertySymbol property
                    && property.Name == name && property.GetMethod is not null && !property.IsIndexer)
                    ?? members.OfType<IFieldSymbol>().FirstOrDefault(field => field.GetBindMode() is not BindMode.None
                        && (field.IsConst ? field.Name : field.GetPropertyName()) == name);

                if (target is null)
                {
                    Report(InvalidDependency, member, dependency, $"The readable property '{name}' was not found.");
                    continue;
                }

                if (member.GetBindMode() is not (BindMode.OneWay or BindMode.TwoWay))
                    Report(SkippedBinding, member, dependency, "BindAlso requires a source member with OneWay or TwoWay binding.");

                if (target.GetBindMode() is BindMode.OneTime or BindMode.OneWayToSource)
                    Report(SkippedBinding, member, dependency, $"The target '{name}' has no change notifications and will be skipped.");
            }

            if (member is not IMethodSymbol command) continue;
            var commands = attributes.Where(attribute => Is(attribute, "RelayCommandAttribute")).ToArray();
            if (commands.Length is 0) continue;
            if (commands.Length > 1)
                Report(InvalidCommand, member, commands[1], "Only one RelayCommand attribute is allowed.");

            if (!command.ReturnsVoid || command.IsGenericMethod || command.Parameters.Length > 4
                || command.Parameters.Any(parameter => parameter.RefKind is not RefKind.None))
            {
                Report(InvalidCommand, member, commands[0], "A command must be a non-generic void method with at most four value parameters.");
                continue;
            }

            foreach (var attribute in commands)
            {
                var name = attribute.NamedArguments.FirstOrDefault(argument => argument.Key == "CanExecute").Value.Value as string;
                if (name is null) continue;
                if (!CommandPredicateResolver.TryResolve(viewModel, command, name, compilation, out _, out _))
                    Report(InvalidPredicate, member, attribute,
                        $"CanExecute '{name}' must name an accessible bool field, bool getter or an unambiguous compatible bool method.");
            }
        }

        return diagnostics.ToImmutable();

        void Report(DiagnosticDescriptor rule, ISymbol member, AttributeData attribute, string reason)
        {
            var location = attribute.ApplicationSyntaxReference?.GetSyntax().GetLocation() ?? member.Locations.FirstOrDefault();
            diagnostics.Add(Diagnostic.Create(rule, location, member.Name, reason));
        }
    }

    private static bool IsAccess(object? value) =>
        value is int access && (SyntaxKind)access is SyntaxKind.PublicKeyword or SyntaxKind.PrivateKeyword or SyntaxKind.ProtectedKeyword;

    private static bool IsBind(AttributeData attribute) =>
        Is(attribute, "BindAttribute") || Is(attribute, "OneWayBindAttribute") || Is(attribute, "TwoWayBindAttribute")
        || Is(attribute, "OneTimeBindAttribute") || Is(attribute, "OneWayToSourceBindAttribute");

    private static bool Is(AttributeData attribute, string name) =>
        attribute.AttributeClass?.ToDisplayString() == "Aspid.MVVM." + name;

    private static DiagnosticDescriptor Rule(string id, string title, DiagnosticSeverity severity) =>
        new(id, title, "'{0}': {1}", "Aspid.MVVM", severity, isEnabledByDefault: true);
}
