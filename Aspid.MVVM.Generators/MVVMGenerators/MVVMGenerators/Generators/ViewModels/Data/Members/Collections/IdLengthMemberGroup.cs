using System.Collections.Immutable;

namespace MVVMGenerators.Generators.ViewModels.Data.Members.Collections;

public readonly struct IdLengthMemberGroup(int length, ImmutableArray<HasCodeMemberGroup> hashCodeGroup)
{
    public readonly int Length = length;
    public readonly ImmutableArray<HasCodeMemberGroup> HashCodeGroup = hashCodeGroup;
}