#nullable enable

using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using BindMode = Aspid.MVVM.Generators.Generators.ViewModels.Data.BindMode;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Extensions;

public static class PropertyNotificationAnalyzer
{
    private static readonly DiagnosticDescriptor AmbiguousCall = new(
        "ASPIDVM007", "Ambiguous property notification", "{0}", "Aspid.MVVM", DiagnosticSeverity.Error, true);
    private static readonly DiagnosticDescriptor InvalidCall = new(
        "ASPIDVM008", "Invalid property notification", "{0}", "Aspid.MVVM", DiagnosticSeverity.Error, true);

    internal static PropertyNotificationData Analyze(
        ITypeSymbol symbol, Compilation compilation, CancellationToken cancellationToken)
    {
        var data = new PropertyNotificationData();
        var targets = new Dictionary<string, (ITypeSymbol Type, bool CanSet, bool CanNotify)>();
        foreach (var member in symbol.GetMembers())
        {
            var mode = member.GetBindMode();
            if (mode is BindMode.None) continue;
            if (member is IPropertySymbol property && !property.IsIndexer)
                targets[property.Name] = (property.Type, !property.IsReadOnly && !property.IsWriteOnly
                    && mode is not BindMode.OneTime, mode is BindMode.OneWay or BindMode.TwoWay);
            else if (member is IFieldSymbol field)
                targets[field.IsConst ? field.Name : field.GetPropertyName()] =
                    (field.Type, false, mode is BindMode.OneWay or BindMode.TwoWay);
        }

        var calls = new List<(string Method, string Type, int Line, string Property, Location Location)>();
        foreach (var reference in symbol.DeclaringSyntaxReferences)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (reference.GetSyntax(cancellationToken) is not TypeDeclarationSyntax declaration) continue;
            var model = compilation.GetSemanticModel(declaration.SyntaxTree);
            foreach (var invocation in declaration.DescendantNodes().OfType<InvocationExpressionSyntax>())
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (invocation.Ancestors().OfType<TypeDeclarationSyntax>().First() != declaration) continue;
                if (invocation.Expression is MemberAccessExpressionSyntax access && access.Expression is not ThisExpressionSyntax)
                    continue;
                var expression = invocation.Expression is MemberAccessExpressionSyntax memberAccess
                    ? memberAccess.Name
                    : invocation.Expression;
                var method = expression is SimpleNameSyntax simpleName ? simpleName.Identifier.ValueText : null;
                if (method is not ("SetField" or "OnPropertyChanged")) continue;
                if (model.GetSymbolInfo(invocation, cancellationToken).Symbol is IMethodSymbol) continue;
                if (expression is GenericNameSyntax)
                {
                    data.Diagnostics.Add(Diagnostic.Create(InvalidCall, invocation.GetLocation(),
                        "Use a typed SetField overload without explicit generic arguments."));
                    continue;
                }
                var isSet = method == "SetField";
                var args = invocation.ArgumentList.Arguments;
                var offset = isSet ? 2 : 0;
                var nameArg = args.FirstOrDefault(arg => arg.NameColon?.Name.Identifier.ValueText == "propertyName");
                var lineArg = args.FirstOrDefault(arg => arg.NameColon?.Name.Identifier.ValueText == "line");
                if (args.Count > offset && args[offset].NameColon is null)
                {
                    var arg = args[offset];
                    if (model.GetTypeInfo(arg.Expression, cancellationToken).Type?.SpecialType is SpecialType.System_Int32)
                        lineArg ??= arg;
                    else
                        nameArg ??= arg;
                }
                if (nameArg is not null && args.Count > offset + 1 && args[offset + 1].NameColon is null)
                    lineArg ??= args[offset + 1];

                string? propertyName = null;
                var dynamicName = false;
                if (nameArg is not null)
                {
                    var constant = model.GetConstantValue(nameArg.Expression, cancellationToken);
                    if (constant.HasValue)
                        propertyName = constant.Value as string;
                    else if (nameArg.Expression is InvocationExpressionSyntax { Expression: IdentifierNameSyntax { Identifier.ValueText: "nameof" } } nameOf)
                        propertyName = nameOf.ArgumentList.Arguments.FirstOrDefault()?.Expression.ToString().Split('.').Last().TrimStart('@');
                    else
                        dynamicName = true;
                }
                else
                    propertyName = invocation.Ancestors().OfType<PropertyDeclarationSyntax>().FirstOrDefault()?.Identifier.ValueText;

                var fieldArg = args.FirstOrDefault(arg => arg.NameColon?.Name.Identifier.ValueText == "field")
                    ?? args.FirstOrDefault(arg => arg.NameColon is null);
                var fieldType = isSet && fieldArg is not null
                    ? model.GetTypeInfo(fieldArg.Expression, cancellationToken).Type
                    : null;
                var matches = targets.Where(pair => (dynamicName || pair.Key == propertyName)
                    && (isSet ? pair.Value.CanSet && SymbolEqualityComparer.Default.Equals(fieldType, pair.Value.Type) : pair.Value.CanNotify)).ToArray();
                if (matches.Length == 0)
                {
                    data.Diagnostics.Add(Diagnostic.Create(InvalidCall, invocation.GetLocation(),
                        $"{method}: specify a supported property name and matching field type; use a direct property helper when needed."));
                    continue;
                }

                var line = invocation.GetLocation().GetMappedLineSpan().StartLinePosition.Line + 1;
                if (nameArg is null && lineArg is not null)
                {
                    var constant = model.GetConstantValue(lineArg.Expression, cancellationToken);
                    if (constant.Value is not int explicitLine)
                    {
                        data.Diagnostics.Add(Diagnostic.Create(InvalidCall, lineArg.GetLocation(),
                            "A line-only notification requires a constant line argument; use an explicit property name instead."));
                        continue;
                    }
                    line = explicitLine;
                }

                foreach (var match in matches)
                {
                    var name = match.Key;
                    var type = match.Value.Type.ToDisplayStringGlobal();
                    if (isSet)
                        data.PropertiesRequiringSetFieldBody.Add(name);
                    if (nameArg is not null)
                    {
                        if (!isSet)
                            data.NamedOnPropertyChangedCalls.Add(name);
                        else
                        {
                            if (!data.NamedSetFieldCallsByType.TryGetValue(type, out var names))
                                data.NamedSetFieldCallsByType[type] = names = [];
                            names.Add(name);
                        }
                    }
                    else
                    {
                        if (isSet)
                            data.AddSetFieldCall(name, line, type);
                        else
                            data.AddOnPropertyChangedCall(name, line);
                        calls.Add((method, isSet ? type : "", line, name, invocation.GetLocation()));
                    }
                }
            }
        }

        foreach (var group in calls.GroupBy(call => (call.Method, call.Type, call.Line)))
        {
            if (group.Select(call => call.Property).Distinct().Count() < 2) continue;
            foreach (var call in group)
                data.Diagnostics.Add(Diagnostic.Create(AmbiguousCall, call.Location,
                    $"{call.Method}: line {call.Line} identifies multiple properties. Pass propertyName explicitly or use the direct property helper."));
        }
        return data;
    }
}
