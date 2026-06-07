#nullable enable
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="UnityGenericOneWayBinder{T}"/> pre-configured with <see cref="BindMode.OneTime"/>,
    /// applying the bound value exactly once.
    /// </summary>
    /// <typeparam name="T">The type of the value to bind.</typeparam>
    /// <remarks>
    /// Unity-specific variant of <see cref="GenericOneTimeBinder{T}"/> that accepts a <see cref="UnityAction{T}"/>.
    /// The setter is called only for the first value pushed from the ViewModel.
    /// </remarks>
    /// <include file="XmlExampleDoc-UnityGenerics-1.1.0.xml" path="doc//member[@name='UnityGenericOneTimeBinder{1}']/*" />
    public class UnityGenericOneTimeBinder<T> : UnityGenericOneWayBinder<T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnityGenericOneTimeBinder{T}"/>.
        /// </summary>
        /// <param name="setValue">The <see cref="UnityAction{T}"/> invoked once with the bound value.</param>
        public UnityGenericOneTimeBinder(UnityAction<T?> setValue)
            : base(setValue, BindMode.OneTime) { }
    }

    /// <summary>
    /// <see cref="UnityGenericOneWayBinder{TTarget,T}"/> pre-configured with <see cref="BindMode.OneTime"/>,
    /// applying the bound value exactly once.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is being set.</typeparam>
    /// <typeparam name="T">The type of the value to bind.</typeparam>
    /// <remarks>
    /// Unity-specific variant of <see cref="GenericOneTimeBinder{TTarget,T}"/> that accepts a <see cref="UnityAction{T0,T1}"/>.
    /// The setter is called only for the first value pushed from the ViewModel.
    /// </remarks>
    /// <include file="XmlExampleDoc-UnityGenerics-1.1.0.xml" path="doc//member[@name='UnityGenericOneTimeBinder{2}']/*" />
    public class UnityGenericOneTimeBinder<TTarget, T> : UnityGenericOneWayBinder<TTarget, T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnityGenericOneTimeBinder{TTarget,T}"/>.
        /// </summary>
        /// <param name="target">The target object whose property is updated.</param>
        /// <param name="setValue">The <see cref="UnityAction{T0,T1}"/> invoked once with the target and the bound value.</param>
        public UnityGenericOneTimeBinder(TTarget target, UnityAction<TTarget, T?> setValue)
            : base(target, setValue, BindMode.OneTime) { }
    }
}