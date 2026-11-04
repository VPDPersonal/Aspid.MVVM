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
            ? $"__viewBindings.BindSafely({name}, viewModel.{bindableMemberName}, this, {member.Id});"
            : $"__viewBindings.BindSafely({name}, viewModel.FindBindableMember({parameters}), this, {member.Id});");
    }
}
