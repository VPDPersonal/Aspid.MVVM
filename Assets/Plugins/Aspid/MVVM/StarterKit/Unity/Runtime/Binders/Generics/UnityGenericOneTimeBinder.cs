#nullable enable
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public class UnityGenericOneTimeBinder<T> : UnityGenericOneWayBinder<T>
    {
        public UnityGenericOneTimeBinder(UnityAction<T?> setValue)
            : base(setValue, BindMode.OneTime) { }
    }
    public class UnityGenericOneTimeBinder<TTarget, T> : UnityGenericOneWayBinder<TTarget, T>
    {
        public UnityGenericOneTimeBinder(TTarget target, UnityAction<TTarget, T?> setValue)
            : base(target, setValue, BindMode.OneTime) { }
    }
}