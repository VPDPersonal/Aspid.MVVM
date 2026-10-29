using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="Binder"/> that operates on a serialized <typeparamref name="TTarget"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of the object the binder operates on.</typeparam>
    [Serializable]
    public abstract class TargetBinder<TTarget> : Binder
    {
        /// <summary>
        /// Gets the object the binder operates on.
        /// </summary>
        [field: Tooltip("The object the binder operates on.")]
        [field: SerializeField]
        protected TTarget Target { get; private set; }

        /// <summary>
        /// Gets a value indicating whether <see cref="Target"/> is set and, for a Unity object, not destroyed.
        /// </summary>
        public override bool CanBind => IsTargetAlive(Target);

        /// <summary>
        /// Creates a binder without a target for Unity deserialization, which then restores the serialized fields.
        /// </summary>
        protected TargetBinder() { }

        /// <summary>
        /// Creates a binder for <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The object to bind.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentNullException"><paramref name="target"/> is <see langword="null"/>.</exception>
        protected TargetBinder(TTarget target, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }

        // A destroyed Unity object is not null to C#; only Unity's own bool conversion sees it.
        private static bool IsTargetAlive(TTarget target)
        {
            if (target is UnityEngine.Object unityObject) return unityObject;
            return target is not null;
        }
    }
}
