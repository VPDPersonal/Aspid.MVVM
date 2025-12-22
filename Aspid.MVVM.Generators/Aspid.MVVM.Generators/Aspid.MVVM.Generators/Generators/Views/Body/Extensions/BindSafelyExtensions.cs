using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.Views.Data.Members;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

namespace Aspid.MVVM.Generators.Generators.Views.Body.Extensions;

public static class BindSafelyExtensions
{
    public static CodeWriter AppendBindSafely(this CodeWriter code, BinderMember member, IBindableMemberInfo bindableMember) =>
        code.AppendBindSafely(member, bindableMember.Bindable.PropertyName);
    
    public static CodeWriter AppendBindSafely(this CodeWriter code, BinderMember member, string? bindableMemberName = null)
    {
        var parameters = $"new({member.Id})";
        var name = member is CachedBinderMember cachedMember ? cachedMember.CachedName : member.Name;

        return code.AppendLine(bindableMemberName is not null 
            ? $"{name}.BindSafely(viewModel.{bindableMemberName});"
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