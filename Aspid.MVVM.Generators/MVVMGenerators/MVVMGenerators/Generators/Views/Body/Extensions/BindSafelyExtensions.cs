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
        var name = member is CachedBinderMember cachedMember ? cachedMember.CachedName : member.Name;

        return code.AppendLine(bindableMember is not null 
            ? $"{name}.BindSafely(viewModel.{bindableMember.GeneratedName});"
            : $"{name}.BindSafely(viewModel.FindBindableMember({parameters}));");

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