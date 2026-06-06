using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="GenericOneWayBinder{T}"/> pre-configured with <see cref="BindMode.OneTime"/>,
    /// applying the bound value exactly once.
    /// </summary>
    /// <typeparam name="T">The type of the value to bind.</typeparam>
    /// <remarks>
    /// The setter is called only for the first value pushed from the ViewModel.
    /// </remarks>
    /// <include file="XmlExampleDoc-Generics-1.1.0.xml" path="doc//member[@name='GenericOneTimeBinder{1}']/*" />
    public class GenericOneTimeBinder<T> : GenericOneWayBinder<T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GenericOneTimeBinder{T}"/>.
        /// </summary>
        /// <param name="setValue">The action invoked once with the bound value.</param>
        public GenericOneTimeBinder(Action<T?> setValue)
            : base(setValue, BindMode.OneTime) { }
    }

    /// <summary>
    /// <see cref="GenericOneWayBinder{TTarget,T}"/> pre-configured with <see cref="BindMode.OneTime"/>,
    /// applying the bound value exactly once.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is being set.</typeparam>
    /// <typeparam name="T">The type of the value to bind.</typeparam>
    /// <remarks>
    /// The setter is called only for the first value pushed from the ViewModel.
    /// </remarks>
    /// <include file="XmlExampleDoc-Generics-1.1.0.xml" path="doc//member[@name='GenericOneTimeBinder{2}']/*" />
    public class GenericOneTimeBinder<TTarget, T> : GenericOneWayBinder<TTarget, T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GenericOneTimeBinder{TTarget,T}"/>.
        /// </summary>
        /// <param name="target">The target object whose property is updated.</param>
        /// <param name="setValue">The action invoked once with the target and the bound value.</param>
        public GenericOneTimeBinder(TTarget target, Action<TTarget, T?> setValue)
            : base(target, setValue, BindMode.OneTime) { }
    }
}