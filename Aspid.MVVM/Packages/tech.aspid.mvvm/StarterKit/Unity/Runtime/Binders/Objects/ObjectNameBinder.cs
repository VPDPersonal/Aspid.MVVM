#nullable enable
using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;Object, string&gt;</see> that binds the
    /// <see cref="Object.name"/> of the target object.
    /// </summary>
    /// <remarks>
    /// A <see langword="null"/> name is written as an empty string, which is what Unity stores for it anyway.
    /// </remarks>
    [Serializable]
    public sealed class ObjectNameBinder : TargetBinder<Object, string>
    {
        /// <param name="target">The <see cref="GameObject"/> whose <see cref="Object.name"/> is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ObjectNameBinder(GameObject target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <param name="target">The <see cref="GameObject"/> whose <see cref="Object.name"/> is bound.</param>
        /// <param name="converter">
        /// An optional converter applied to the value before it is applied. Pass <see langword="null"/> to use the
        /// value unchanged. Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the name raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ObjectNameBinder(GameObject target, IConverter<string?, string?>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected override string? Property
        {
            get => Target.name;
            set => Target.name = value ?? string.Empty;
        }
    }
}
