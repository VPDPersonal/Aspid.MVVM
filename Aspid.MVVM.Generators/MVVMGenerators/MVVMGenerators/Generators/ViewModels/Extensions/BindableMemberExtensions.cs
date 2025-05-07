using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.ViewModels.Data;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.ViewModels.Extensions;

public static class BindableMemberExtensions
{
    public static CodeWriter AppendBindableMemberInstance(this CodeWriter code, BindableMember member)
    {
        if (member.Mode is BindMode.None) return code;
        
        var viewModelEvent = member switch
        {
            BindableField bindableField => bindableField.Event,
            BindableBindAlso bindableBindAlso => bindableBindAlso.Event,
            _ => default
        };
                    
        code.Append($"{Classes.BindableMember}<{member.Type}>.");
                    
        switch (member.Mode)
        {
            case BindMode.OneWay:
                {
                    code.Append($"OneWay({viewModelEvent.FieldName} ??= new(), {member.SourceName})");
                    break;
                }
                        
            case BindMode.TwoWay: 
                {
                    code.Append($"TwoWay({viewModelEvent.FieldName} ??= new(Set{member.GeneratedName}), {member.SourceName})");
                    break;
                }
                        
            case BindMode.OneTime: 
                {
                    code.Append($"OneTime({member.SourceName})"); 
                    break;
                }
                        
            case BindMode.OneWayToSource: 
                {
                    code.Append($"OneWayToSource({viewModelEvent.FieldName} ??= new(Set{member.GeneratedName}))");
                    break;
                }
        }

        return code;
    }
}