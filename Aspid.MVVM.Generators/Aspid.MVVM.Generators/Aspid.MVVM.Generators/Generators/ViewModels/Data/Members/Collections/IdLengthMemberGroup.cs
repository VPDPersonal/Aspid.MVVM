using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Members.Collections;

public readonly struct IdLengthMemberGroup(int length, ImmutableArray<IBindableMemberInfo> members)
{
    public readonly int Length = length;
    public readonly ImmutableArray<IBindableMemberInfo> Members = members;
    
    public static ImmutableArray<IdLengthMemberGroup> Create(ImmutableArray<IBindableMemberInfo> bindableMembers)
    {
        var bindableMembersCountByLength = new Dictionary<int, int>();
        
        foreach (var bindableMember in bindableMembers)
        {
            var length = bindableMember.Id.Length;
            if (!bindableMembersCountByLength.ContainsKey(length))
                bindableMembersCountByLength[length] = 0;
            
            bindableMembersCountByLength[length] += 1;
        }
        
        var idGroups = new Dictionary<int, List<IBindableMemberInfo>>();

        foreach (var bindableMember in bindableMembers)
        {
            var length = bindableMember.Id.Length;
            if (bindableMembersCountByLength[length] == 1) continue;

            if (!idGroups.TryGetValue(length, out var bindableMembersList))
            {
                bindableMembersList = [];
                idGroups[length] = bindableMembersList;
            }
                
            bindableMembersList?.Add(bindableMember);
        }
        
        return idGroups.Select(idGroup => 
                new IdLengthMemberGroup(idGroup.Key, idGroup.Value.ToImmutableArray()))
            .ToImmutableArray();
    }
}