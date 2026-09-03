#nullable enable
using System;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget, TProperty}">TargetBinder&lt;Object, string&gt;</see> that binds <see cref="Object.name"/> of the target.
    /// </summary>
    /// <remarks>
    /// A <see langword="null"/> name is written as an empty string, which is what Unity stores for it anyway.
    /// </remarks>
    [Serializable]
    public sealed class ObjectNameBinder : TargetBinder<Object, string>
    {
        /// <param name="target">The object whose <see cref="Object.name"/> is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>: the name raises no change event.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ObjectNameBinder(Object target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <param name="target">The object whose <see cref="Object.name"/> is bound.</param>
        /// <param name="converter">
        /// The converter applied before the value is written, or <see langword="null"/> to use it unchanged.
        /// Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>: the name raises no change event.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ObjectNameBinder(
            Object target,
            IConverter<string?, string?>? converter = null,
            BindMode mode = BindMode.OneWay)
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
