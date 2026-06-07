using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Aspid.MVVM.Generators.Generators.Views.Data.Members;

public class AsBinderMember(ISymbol member, string asBinderType, IReadOnlyList<string>? arguments) : CachedBinderMember(member)
{
    public readonly string AsBinderType = asBinderType;
    
    public readonly string Arguments = arguments is null || arguments.Count is 0 
            ? string.Empty 
            : ", " + string.Join(", ", arguments);
}