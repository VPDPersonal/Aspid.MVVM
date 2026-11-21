using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Helpers;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using Aspid.MVVM.Generators.Generators.ViewModels.Extensions;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Factories;

public static class BindableMembersFactory
{
    public static ImmutableArray<IBindableMemberInfo> Create(
        ITypeSymbol symbol,
        Compilation compilation,
        out PropertyNotificationData propertyNotificationData,
        CancellationToken cancellationToken = default)
    {
        var members = new MembersByGroup(symbol);
        propertyNotificationData = PropertyNotificationAnalyzer.Analyze(symbol, compilation, cancellationToken);

        if (propertyNotificationData.Diagnostics.Any(diagnostic => diagnostic.Severity is DiagnosticSeverity.Error))
            return ImmutableArray<IBindableMemberInfo>.Empty;

        var bindableFields = BindableFieldFactory.Create(members.Fields);
        var bindableBindAlso = BindableBindAlsoFactory.Create(members.All);
        var bindableProperties = BindablePropertyFactory.Create(symbol, members.Properties, propertyNotificationData, cancellationToken);
        
        var bindableCommands = BindableCommandFactory.Create(members.Methods, compilation);

        var filteredBindableBindAlso = bindableBindAlso.Where(b => !b.HasBindAttribute).ToList();
        var bindableMembers = new List<IBindableMemberInfo>(filteredBindableBindAlso.Count + bindableFields.Count + bindableProperties.Count + bindableCommands.Count);
        
        bindableMembers.AddRange(bindableFields);
        bindableMembers.AddRange(bindableProperties);
        bindableMembers.AddRange(bindableCommands);
        bindableMembers.AddRange(filteredBindableBindAlso);
            
        return bindableMembers.ToImmutableArray();
    }
    
}
