using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base class for binders that operate on a specific target object.
    /// Extends <see cref="Binder"/> with a typed <typeparamref name="TTarget"/> reference
    /// that is available to derived classes for binding logic.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that this binder operates on.</typeparam>
    [Serializable]
    public abstract class TargetBinder<TTarget> : Binder
    {
        /// <summary>
        /// Gets the target object this binder is associated with.
        /// </summary>
#if UNITY_2022_1_OR_NEWER
        [field: UnityEngine.SerializeField]
#endif
        protected TTarget Target { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetBinder{TTarget}"/> class with the specified target and binding mode.
        /// </summary>
        /// <param name="target">The target object this binder will operate on.</param>
        /// <param name="mode">The binding mode to use for the binder.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is <c>null</c>.</exception>
        protected TargetBinder(TTarget target, BindMode mode)
            : base(mode)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }
    }
}