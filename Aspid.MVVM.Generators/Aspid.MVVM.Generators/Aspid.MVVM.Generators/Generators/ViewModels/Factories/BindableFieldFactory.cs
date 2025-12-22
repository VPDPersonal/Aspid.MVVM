using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using Aspid.MVVM.Generators.Generators.ViewModels.Extensions;
using BindMode = Aspid.MVVM.Generators.Generators.ViewModels.Data.BindMode;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

public static class BindableFieldFactory
{
    public static IReadOnlyCollection<BindableFieldInfo> Create(ImmutableArray<IFieldSymbol> fields)
    {
        var bindableFields = new List<BindableFieldInfo>();

        foreach (var field in fields)
        {
            var mode = field.GetBindMode();

            switch (mode)
            {
                case BindMode.OneTime: break;

                case BindMode.OneWay:
                case BindMode.TwoWay:
                case BindMode.OneWayToSource:
                    {
                        if (field.IsReadOnly) continue;
                        break;
                    }

                case BindMode.None:
                default: continue;
            }
            
            bindableFields.Add(new BindableFieldInfo(field, mode));
        }

        return bindableFields;
    }
}