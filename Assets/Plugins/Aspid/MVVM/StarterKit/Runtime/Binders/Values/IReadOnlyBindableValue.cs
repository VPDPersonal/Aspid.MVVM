using System;

namespace Aspid.MVVM.StarterKit
{
    public interface IReadOnlyBindableValue<out T>
    {
        public event Action<T?>? Changed;
        
        public T? Value { get; }
        
        public BindMode Mode { get; }
    }
}