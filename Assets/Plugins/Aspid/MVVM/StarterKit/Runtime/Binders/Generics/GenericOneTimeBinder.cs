#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class GenericOneTimeBinder<T> : GenericOneWayBinder<T>
    {
        public GenericOneTimeBinder(Action<T?> setValue)
            : base(setValue, BindMode.OneTime) { }
    }
    
    public class GenericOneTimeBinder<TTarget, T> : GenericOneWayBinder<TTarget, T>
    {
        public GenericOneTimeBinder(TTarget target, Action<TTarget, T?> setValue)
            : base(target, setValue, BindMode.OneTime) { }
    }
}