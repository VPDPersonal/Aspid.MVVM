using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using Aspid.MVVM.Generators.Generators.ViewModels.Extensions;
using BindMode = Aspid.MVVM.Generators.Generators.ViewModels.Data.BindMode;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

public static class BindablePropertyFactory
{
    public static IReadOnlyCollection<BindablePropertyInfo> Create(ImmutableArray<IPropertySymbol> properties)
    {
        var bindableProperties = new List<BindablePropertyInfo>();

        foreach (var property in properties)
        {
            var mode = property.GetBindMode();

            switch (mode)
            {
                case BindMode.OneTime:
                case BindMode.OneWay: break;

                case BindMode.TwoWay:
                case BindMode.OneWayToSource:
                    {
                        if (property.IsReadOnly) continue;
                        break;
                    }
                
                default: continue;
            }
            
            bindableProperties.Add(new BindablePropertyInfo(property, mode));
        }

        return bindableProperties;
    }
}
