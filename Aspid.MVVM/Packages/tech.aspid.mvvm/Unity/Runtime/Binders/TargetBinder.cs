using System;
using UnityEngine;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="Binder"/> with a serialized target.
    /// </summary>
    /// <typeparam name="TTarget">The type of object to bind.</typeparam>
    [Serializable]
    public abstract class TargetBinder<TTarget> : Binder
    {
        /// <summary>
        /// Gets the target assigned at construction or restored by Unity.
        /// </summary>
        [field: Tooltip("The object to bind.")]
        [field: SerializeField]
        protected TTarget Target { get; private set; }

        /// <summary>
        /// Creates a binder whose target is restored by Unity deserialization.
        /// </summary>
        protected TargetBinder() { }

        /// <summary>
        /// Creates a binder for <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The object to bind.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode"/> is <see cref="BindMode.None"/> or undefined.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="target"/> is <see langword="null"/>.</exception>
        protected TargetBinder(TTarget target, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Called before attachment to validate the required target.
        /// </summary>
        /// <remarks>Overrides must call the base implementation before acquiring resources.</remarks>
        /// <exception cref="InvalidOperationException">The target is missing or destroyed.</exception>
        protected override void OnBinding()
        {
            if (!Target.IsMissing()) return;
            throw new InvalidOperationException("Assign a valid target before binding.");
        }
    }
}
