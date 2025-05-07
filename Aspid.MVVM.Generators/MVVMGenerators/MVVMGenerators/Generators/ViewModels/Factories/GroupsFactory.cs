using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using MVVMGenerators.Generators.ViewModels.Data.Members;
using MVVMGenerators.Generators.ViewModels.Data.Members.Collections;

namespace MVVMGenerators.Generators.ViewModels.Factories;

public static class GroupsFactory
{
    public static (ImmutableArray<HasCodeMemberGroup> hashCodeGroup, ImmutableArray<IdLengthMemberGroup> idLengthGroups)
        Create(ImmutableArray<BindableMember> bindableMembers)
    {
        var bindableMembersCountByLength = new Dictionary<int, int>();
        
        foreach (var bindableMember in bindableMembers)
        {
            var length = bindableMember.Id.Length;
            if (!bindableMembersCountByLength.ContainsKey(length))
                bindableMembersCountByLength[length] = 0;
            
            bindableMembersCountByLength[length] += 1;
        }
        
        var hashCodeGroups = new Dictionary<int, List<BindableMember>>();
        var idGroups = new Dictionary<int, Dictionary<int, List<BindableMember>>>();

        foreach (var bindableMember in bindableMembers)
        {
            var length = bindableMember.Id.Length;
            var hashCode = bindableMember.Id.HashCode;

            List<BindableMember>? bindableMembersList;
            
            if (bindableMembersCountByLength[length] == 1)
            {
                if (!hashCodeGroups.TryGetValue(hashCode, out bindableMembersList))
                {
                    bindableMembersList = [];
                    hashCodeGroups[hashCode] = bindableMembersList;
                }
            }
            else
            {
                if (!idGroups.TryGetValue(length, out var hasCodeGroup))
                {
                    hasCodeGroup = new Dictionary<int, List<BindableMember>>();
                    idGroups[length] = hasCodeGroup;
                }

                if (!hasCodeGroup.TryGetValue(hashCode, out bindableMembersList))
                {
                    bindableMembersList = [];
                    hasCodeGroup[hashCode] = bindableMembersList;
                }
            }
            
            bindableMembersList?.Add(bindableMember);
        }
        
        var idGroupsList = new List<IdLengthMemberGroup>();
        var hashCodeGroupsList = new List<HasCodeMemberGroup>();
        
        foreach (var idGroup in idGroups)
        {
            var hashCodeGroup = new List<HasCodeMemberGroup>(idGroup.Value.Select(membersByHashCodePair => 
                new HasCodeMemberGroup(membersByHashCodePair.Key, membersByHashCodePair.Value.ToImmutableArray())));
            
            idGroupsList.Add(new IdLengthMemberGroup(idGroup.Key, hashCodeGroup.ToImmutableArray()));
        }
        
        foreach (var hashCodeGroup in hashCodeGroups)
            hashCodeGroupsList.Add(new HasCodeMemberGroup(hashCodeGroup.Key, hashCodeGroup.Value.ToImmutableArray()));

        return (hashCodeGroupsList.ToImmutableArray(), idGroupsList.ToImmutableArray());
    }
}
