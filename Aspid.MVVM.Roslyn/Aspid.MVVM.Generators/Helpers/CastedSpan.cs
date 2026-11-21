using System;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM.Generators.Helpers;

public ref struct CastedSpan<TFrom, TTo>(ReadOnlySpan<TFrom> span)
{
    public readonly ReadOnlySpan<TFrom> Span = span;
    private int _index = -1;
        
    public int Length => Span.Length;

    public bool IsEmpty => Span.IsEmpty;
        
    public TTo Current => this[_index];

    public TTo this[int index]
    {
        get
        {
            var member = Span[index];
            return Unsafe.As<TFrom, TTo>(ref member);
        }
    }

    public CastedSpan<TFrom, TTo> GetEnumerator() =>
        this;

    public bool MoveNext()
    {
        var index = _index + 1;
        if (index >= Span.Length) return false;

        _index = index;
        return true;
    }

    public void Reset() => _index = -1;
}