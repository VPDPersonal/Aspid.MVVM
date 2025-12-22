using System.Linq;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using Aspid.MVVM.Generators.Generators.ViewModels.Extensions;
using BindMode = Aspid.MVVM.Generators.Generators.ViewModels.Data.BindMode;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

public static class BindablePropertyFactory
{
    public static IReadOnlyCollection<BindablePropertyInfo> Create(
        TypeDeclarationSyntax declaration, 
        ImmutableArray<IPropertySymbol> properties,
        PropertyNotificationData propertyNotificationData)
    {
        var bindableProperties = new List<BindablePropertyInfo>();
        
        var setMethodNames = new HashSet<string>(properties
            .Select(p => $"Set{p.Name}Field"));
        
        var usedSetMethods = MethodUsageAnalyzer.GetUsedMethods(declaration, setMethodNames);

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
            
            var setMethodName = $"Set{property.Name}Field";
            var isSetMethodUsed = usedSetMethods.Contains(setMethodName) 
                || propertyNotificationData.PropertiesRequiringSetFieldBody.Contains(property.Name);
            
            bindableProperties.Add(new BindablePropertyInfo(property, mode, isSetMethodUsed));
        }

        return bindableProperties;
    }
}
