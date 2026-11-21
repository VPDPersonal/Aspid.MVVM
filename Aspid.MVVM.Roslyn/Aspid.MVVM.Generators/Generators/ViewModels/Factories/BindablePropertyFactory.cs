using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using Aspid.MVVM.Generators.Generators.ViewModels.Extensions;
using BindMode = Aspid.MVVM.Generators.Generators.ViewModels.Data.BindMode;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

public static class BindablePropertyFactory
{
    public static IReadOnlyCollection<BindablePropertyInfo> Create(
        ITypeSymbol symbol,
        ImmutableArray<IPropertySymbol> properties,
        PropertyNotificationData propertyNotificationData,
        CancellationToken cancellationToken)
    {
        var bindableProperties = new List<BindablePropertyInfo>();

        var setMethodNames = new HashSet<string>(properties
            .Select(p => $"Set{p.Name.CapitalizeFirstLetter()}Field"));

        var usedSetMethods = new HashSet<string>();
        foreach (var reference in symbol.DeclaringSyntaxReferences)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (reference.GetSyntax(cancellationToken) is TypeDeclarationSyntax declaration)
                usedSetMethods.UnionWith(MethodUsageAnalyzer.GetUsedMethods(declaration, setMethodNames, cancellationToken));
        }

        foreach (var property in properties)
        {
            var mode = property.GetBindMode();

            switch (mode)
            {
                case BindMode.OneTime:
                case BindMode.OneWay:
                    {
                        if (property.IsWriteOnly) continue;
                        break;
                    }
                case BindMode.TwoWay:
                    {
                        if (property.IsReadOnly || property.IsWriteOnly) continue;
                        break;
                    }
                case BindMode.OneWayToSource:
                    {
                        if (property.IsReadOnly) continue;
                        break;
                    }

                case BindMode.None:
                default: continue;
            }

            var setMethodName = $"Set{property.Name.CapitalizeFirstLetter()}Field";
            var isSetMethodUsed = usedSetMethods.Contains(setMethodName)
                || propertyNotificationData.PropertiesRequiringSetFieldBody.Contains(property.Name);
            
            bindableProperties.Add(new BindablePropertyInfo(property, mode, isSetMethodUsed));
        }

        return bindableProperties;
    }
}