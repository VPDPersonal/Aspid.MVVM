using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;

namespace Aspid.MVVM.Generators.Views.Data.Members;

public class CachedBinderMember(ISymbol member) : BinderMember(member)
{
    public readonly string CachedName = member switch
    {
        IFieldSymbol => $"_{member.Name}CachedBinder",
        IPropertySymbol => $"_{member.GetFieldName()}CachedPropertyBinder",
        _ => string.Empty
    };
}