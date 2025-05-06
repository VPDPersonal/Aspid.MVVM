using System.Collections.Immutable;

namespace MVVMGenerators.Generators.ViewModels.Data.Members.Collections;

public readonly struct HasCodeMemberGroup(int hashCode, ImmutableArray<BindableMember> members)
{
    public readonly int HashCode = hashCode;
    public readonly ImmutableArray<BindableMember> Members = members;
}