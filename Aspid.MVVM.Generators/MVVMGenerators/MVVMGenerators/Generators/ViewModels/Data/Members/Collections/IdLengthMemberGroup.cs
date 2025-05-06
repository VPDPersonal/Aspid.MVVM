using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MVVMGenerators.Generators.ViewModels.Data.Members.Collections;

public readonly struct IdLengthMemberGroup(int length, ImmutableArray<HasCodeMemberGroup> hashCodeGroup)
{
    public readonly int Length = length;
    public readonly ImmutableArray<HasCodeMemberGroup> HashCodeGroup = hashCodeGroup;

    public static ImmutableArray<IdLengthMemberGroup> CreateGroups(ImmutableArray<BindableMember> bindableMembers)
    {
        var members = new Dictionary<int, Dictionary<int, List<BindableMember>>>();
        
        foreach (var bindableMember in bindableMembers)
        {
            var length = bindableMember.Id.Length;
            var hashCode = bindableMember.Id.HashCode;

            if (!members.TryGetValue(length, out var hashCodeDictionary))
            {
                hashCodeDictionary = new Dictionary<int, List<BindableMember>>();
            }

            if (!hashCodeDictionary.TryGetValue(hashCode, out var membersByHashCode))
            {
                membersByHashCode = [];
                hashCodeDictionary.Add(hashCode, membersByHashCode);
            }
                
            membersByHashCode.Add(bindableMember);
            members[length] = hashCodeDictionary;
        }

        var idGroups = new List<IdLengthMemberGroup>(members.Count);

        foreach (var memberPair in members)
        {
            var hashCodeGroup = new List<HasCodeMemberGroup>(memberPair.Value.Select(membersByHashCodePair => 
                new HasCodeMemberGroup(membersByHashCodePair.Key, membersByHashCodePair.Value.ToImmutableArray())));
            
            idGroups.Add(new IdLengthMemberGroup(memberPair.Key, hashCodeGroup.ToImmutableArray()));
        }

        return idGroups.ToImmutableArray();
    }
}