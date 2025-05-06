using System;
using System.Linq;
using MVVMGenerators.Helpers.Data;
using System.Collections.Immutable;

namespace MVVMGenerators.Generators.ViewModels.Data.Members.Collections;

public readonly ref struct BindableMembersCollectionSpanByType
{
    public readonly CastedSpan<BindableMember, BindableField> Fields;
    public readonly CastedSpan<BindableMember, BindableCommand> Commands;
    public readonly CastedSpan<BindableMember, BindableBindAlso> BindAlso;
    
    public BindableMembersCollectionSpanByType(ImmutableArray<BindableMember> members)
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

                if (type == typeof(BindableField))
                {
                    Fields = new CastedSpan<BindableMember, BindableField>(span.Slice(startIndex, length));
                }
                else if (type == typeof(BindableCommand))
                {
                    Commands = new CastedSpan<BindableMember, BindableCommand>(span.Slice(startIndex, length));
                }
                else if (type == typeof(BindableBindAlso))
                {
                    BindAlso = new CastedSpan<BindableMember, BindableBindAlso>(span.Slice(startIndex, length));
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

    private static T[] SortByType<T>(T[] input)
    {
        var array = (T[])input.Clone();
        var n = array.Length;

        for (var i = 0; i < n - 1; i++)
        {
            for (var j = 0; j < n - i - 1; j++)
            {
                var typeName1 = array[j]?.GetType();
                var typeName2 = array[j + 1]?.GetType();

                if (typeName1 == typeName2)
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                }
            }
        }

        return array;
    }
}