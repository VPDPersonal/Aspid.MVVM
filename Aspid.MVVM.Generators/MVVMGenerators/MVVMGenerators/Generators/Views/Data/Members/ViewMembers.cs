using System.Collections.Generic;
using System.Collections.Immutable;

namespace MVVMGenerators.Generators.Views.Data.Members;

public readonly struct ViewMembers(
    IEnumerable<BinderFieldInView> fieldMembers,
    IEnumerable<PropertyBinderInView> propertyMembers,
    IEnumerable<AsBinderMemberInView> ssBinderMembers)
{
    public readonly ImmutableArray<BinderFieldInView> Fields = ImmutableArray.CreateRange(fieldMembers);
    public readonly ImmutableArray<PropertyBinderInView> Properties = ImmutableArray.CreateRange(propertyMembers);
    public readonly ImmutableArray<AsBinderMemberInView> AsBinderMembers = ImmutableArray.CreateRange(ssBinderMembers);
}