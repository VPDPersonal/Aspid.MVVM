using System;
using System.Linq;
using System.Collections.Immutable;
using Aspid.MVVM.Generators.Helpers;

namespace Aspid.MVVM.Generators.Generators.Views.Data.Members.Collections;

public readonly ref struct BinderMembersCollectionSpanByType
{
    public readonly ReadOnlySpan<BinderMember> Binders;
    public readonly CastedSpan<BinderMember, AsBinderMember> AsBinders;
    public readonly CastedSpan<BinderMember, CachedBinderMember> PropertyBinders;
    
    public BinderMembersCollectionSpanByType(ImmutableArray<BinderMember> members)
    {
        var span = members.OrderBy(element => element.GetType().ToString()).ToArray().AsSpan();

        if (span.Length > 0)
        {
            var length = 0;
            var startIndex = 0;
            var type = span[0].GetType();
            
            for (var i = 0; i < span.Length; i++)
            {
                if (type == span[i].GetType())
                    length++;
                
                if (i + 1 != span.Length && type == span[i + 1].GetType())
                    continue;

                if (type == typeof(AsBinderMember))
                {
                    AsBinders = new CastedSpan<BinderMember, AsBinderMember>(span.Slice(startIndex, length));
                }
                else if (type == typeof(CachedBinderMember))
                {
                    PropertyBinders = new CastedSpan<BinderMember, CachedBinderMember>(span.Slice(startIndex, length));
                }
                else
                {
                    Binders = span.Slice(startIndex, length);
                }

                if (i + 1 != span.Length)
                {
                    length = 0;
                    startIndex = i + 1;
                    type = span[i + 1].GetType();
                }
            }
        }
    }
}