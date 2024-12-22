#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Binders
{
    public interface IReadOnlyBindableProperty<out T>
    {
        public event Action<T?>? Changed;
        
        public T? Value { get; }
    }
}