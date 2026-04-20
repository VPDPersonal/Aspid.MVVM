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
        var diagnostics = IsBinderCollection(member.Type) ? $", this, {member.Id}" : string.Empty;

        return code.AppendLine(bindableMemberName is not null
            ? $"{name}.BindSafely(viewModel.{bindableMemberName}{diagnostics});"
            : $"{name}.BindSafely(viewModel.FindBindableMember({parameters}){diagnostics});");
    }

    // True when `type` resolves to one of the BindSafely/UnbindSafely collection overloads
    // (T[], List<T>, IEnumerable<T> or anything implementing IEnumerable<T>).
    internal static bool IsBinderCollection(ITypeSymbol? type)
    {
        if (type is null) return false;
        if (type is IArrayTypeSymbol) return true;
        if (type.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T) return true;

        foreach (var i in type.AllInterfaces)
        {
            if (i.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T)
                return true;
        }

        return false;
    }
}
