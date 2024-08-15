using System.Collections.Generic;
using System.Collections.Immutable;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct MembersContainer(
    IEnumerable<FieldMember> fieldMembers,
    IEnumerable<PropertyMember> propertyMembers,
    IEnumerable<AsBinderMember> ssBinderMembers)
{
    public readonly ImmutableArray<FieldMember> FieldMembers = ImmutableArray.CreateRange(fieldMembers);
    public readonly ImmutableArray<PropertyMember> PropertyMembers = ImmutableArray.CreateRange(propertyMembers);
    public readonly ImmutableArray<AsBinderMember> AsBinderMembers = ImmutableArray.CreateRange(ssBinderMembers);
}