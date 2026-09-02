using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base <see cref="Binder"/> that provides a typed <typeparamref name="TTarget"/> reference
    /// available to derived classes for binding logic.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that this binder operates on.</typeparam>
    [Serializable]
    public abstract class TargetBinder<TTarget> : Binder
    {
        /// <summary>
        /// Gets the target object this binder is associated with.
        /// </summary>
        [field: Tooltip("The target object this binder operates on.")]
        [field: SerializeField]
        protected TTarget Target { get; private set; }

        /// <summary>
        /// Indicates whether binding is allowed: <see langword="false"/> when <see cref="Target"/> is missing.
        /// </summary>
        /// <remarks>
        /// The constructor rejects a <see langword="null"/> target, but a serialized instance never runs it — Unity
        /// assigns <see cref="Target"/> directly — so the field can arrive empty, or pointing at an object that has
        /// since been destroyed. Binding on either produces an exception from whatever <c>OnBound</c> touches first,
        /// which names the Unity type rather than the binder or its View. Refusing to bind is quieter and leaves the
        /// rest of the View working.
        /// </remarks>
        public override bool CanBind => IsTargetAlive(Target);

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetBinder{TTarget}"/> class with the specified target and binding mode.
        /// </summary>
        /// <param name="target">The target object this binder will operate on.</param>
        /// <param name="mode">The binding mode to use for the binder.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="target"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// For deserialization only: Unity builds a serialized instance without running a constructor's arguments and
        /// assigns the fields itself.
        /// </remarks>
        protected TargetBinder() { }

        protected TargetBinder(TTarget target, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Reports whether <paramref name="target"/> can still be used.
        /// </summary>
        /// <remarks>
        /// A destroyed <see cref="UnityEngine.Object"/> is not <see langword="null"/> to C# — the managed wrapper
        /// outlives the native object — so a plain <c>is not null</c> accepts a reference that throws on first use.
        /// Unity's own conversion is the only check that sees the difference, and it is only available when the
        /// target really is a Unity object.
        /// </remarks>
        private static bool IsTargetAlive(TTarget target)
        {
#if UNITY_2020_3_OR_NEWER
            if (target is UnityEngine.Object unityObject) return unityObject;
#endif
            return target is not null;
        }
    }
}
