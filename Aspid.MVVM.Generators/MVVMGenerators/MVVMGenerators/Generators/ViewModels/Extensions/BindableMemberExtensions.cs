using MVVMGenerators.Helpers;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Extensions;

public static class BindableMemberExtensions
{
    public static CodeWriter AppendBindableMemberInstance(this CodeWriter code, BindableMember member)
    {
        if (member.Mode is BindMode.None) return code;

        var viewModelEvent = member.Event;

        return member.Mode switch
        {
            BindMode.OneWay => code.Append($"{viewModelEvent.FieldName} ??= new({member.SourceName})"),
            BindMode.TwoWay => code.Append($"{viewModelEvent.FieldName} ??= new({member.SourceName}, Set{member.GeneratedName})"),
            BindMode.OneTime => code.Append($"{viewModelEvent.FieldName} ??= new({member.SourceName})"),
            BindMode.OneWayToSource => code.Append($"{viewModelEvent.FieldName} ??= new(Set{member.GeneratedName})"),
            _ => code
        };

    }
}