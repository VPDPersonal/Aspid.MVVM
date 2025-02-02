using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MVVMGenerators.Generators.ViewModels.Data.Members.Fields;

public readonly struct ViewModelFields : IEnumerable<ViewModelField>
{
    public readonly ImmutableArray<ViewModelField> Values;

    public ViewModelFields(IEnumerable<ViewModelField> fields)
    {
        Values = ImmutableArray.CreateRange(fields.OrderBy(field => field.Mode));
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
    
    public IEnumerator<ViewModelField> GetEnumerator() =>
        ((IEnumerable<ViewModelField>)Values).GetEnumerator();
}