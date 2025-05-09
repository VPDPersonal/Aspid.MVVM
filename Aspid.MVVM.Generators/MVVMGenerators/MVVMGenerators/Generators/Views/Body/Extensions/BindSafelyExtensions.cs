using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.Views.Data.Members;
using MVVMGenerators.Generators.ViewModels.Data.Members;

namespace MVVMGenerators.Generators.Views.Body.Extensions;

public static class BindSafelyExtensions
{
    public static CodeWriter AppendBindSafely(this CodeWriter code, BinderMember member, BindableMember? bindableMember = null)
    {
        var parameters = $"new({member.Id})";
        var bindingType = member.BindingType?.ToDisplayStringGlobal();
        var name = member is CachedBinderMember cachedMember ? cachedMember.CachedName : member.Name;

        if (bindableMember is not null)
        {
            var bindableMemberType = bindableMember.Type;
            
            if (bindingType is null || bindingType == bindableMemberType)
            {
                var binderMemberType = member.GetBinderMemberType();
                return code.AppendLine($"{name}.BindSafely<{binderMemberType}, {bindableMemberType}>(viewModel.{bindableMember.GeneratedName});");
            }
        }

        code.BeginBlock();
        
        if (bindingType is not null)
        {
            var binderMemberType = member.GetBinderMemberType();

            code.AppendLine(
                    $"var __aspidLocalResultGenerated = viewModel.FindBindableMember<{bindingType}>({parameters});")
                .AppendLine("if (__aspidLocalResultGenerated.IsFound)")
                .BeginBlock()
                .AppendLine($"{name}.BindSafely<{binderMemberType}, {bindingType}>(__aspidLocalResultGenerated.Member);");
        }
        else
        {
            code.AppendLine($"var __aspidLocalResultGenerated = viewModel.FindBindableMember({parameters});")
                .AppendLine("if (__aspidLocalResultGenerated.IsFound)")
                .BeginBlock()
                .AppendLine($"{name}.BindSafely(__aspidLocalResultGenerated.Adder);");
        }
        
        return code
            .EndBlock()
            .EndBlock()
            .AppendLine(); 
    }

    private static string GetBinderMemberType(this BinderMember member)
    {
        if (member is AsBinderMember asBinderMember)
            return asBinderMember.AsBinderType;
        
        return member.Type is IArrayTypeSymbol arrayType 
            ? arrayType.ElementType.ToDisplayStringGlobal()
            : member.Type.ToDisplayStringGlobal();
    }
}