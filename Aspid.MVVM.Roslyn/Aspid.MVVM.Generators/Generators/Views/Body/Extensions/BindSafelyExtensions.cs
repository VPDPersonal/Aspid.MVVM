#nullable enable
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
        var name = member is CachedBinderMember cachedMember
            ? cachedMember.CachedName
            : member.Name;

        return bindableMemberName is not null
            ? code.AppendLine($"AspidBinderExtensions.BindSafely({name}, new global::Aspid.MVVM.BindContext(viewModel.{bindableMemberName}, this));")
            : code.AppendLookupBinding(name, member.Id);
    }

    public static CodeWriter AppendLookupBinding(this CodeWriter code, string binder, string id)
    {
        var adder = $"__{binder}Adder";
        return code.BeginBlock()
            .AppendLine($"if (viewModel.FindBindableMember(new({id})).Adder is {{ }} {adder})")
            .BeginBlock()
            .AppendLine($"AspidBinderExtensions.BindSafely({binder}, new global::Aspid.MVVM.BindContext({adder}, this));")
            .EndBlock()
            .EndBlock();
    }
}
