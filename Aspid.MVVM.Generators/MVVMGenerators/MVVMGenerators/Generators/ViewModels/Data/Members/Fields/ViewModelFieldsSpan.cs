using System;

namespace MVVMGenerators.Generators.ViewModels.Data.Members.Fields;

public readonly ref struct ViewModelFieldsSpan
{
    public readonly ReadOnlySpan<ViewModelField> All;
    
    public readonly ReadOnlySpan<ViewModelField> OneWayFields;
    public readonly ReadOnlySpan<ViewModelField> TwoWayFields;
    public readonly ReadOnlySpan<ViewModelField> OneTimeFields;
    public readonly ReadOnlySpan<ViewModelField> OneWayToSourceFields;
    
    public ViewModelFieldsSpan(ViewModelFields fields)
    {
        var span = fields.Values.AsSpan();

        if (span.Length > 0)
        {
            var length = 0;
            var startIndex = 0;
            var mode = span[0].Mode;
            
            for (var i = 0; i < span.Length; i++)
            {
                if (mode == span[i].Mode)
                    length++;
                
                if (i + 1 != span.Length && mode == span[i + 1].Mode)
                    continue;
                
                switch (mode)
                {
                    case BindMode.None: break;
                    case BindMode.OneWay: OneWayFields = span.Slice(startIndex, length); break;
                    case BindMode.TwoWay: TwoWayFields = span.Slice(startIndex, length); break;
                    case BindMode.OneTime: OneTimeFields = span.Slice(startIndex, length); break;
                    case BindMode.OneWayToSource: OneWayToSourceFields = span.Slice(startIndex, length); break;
                    
                    default: throw new ArgumentOutOfRangeException();
                }

                if (i + 1 != span.Length)
                {
                    length = 0;
                    startIndex = i + 1;
                    mode = span[i + 1].Mode;
                }
            }
        }

        All = span; 
    }
}