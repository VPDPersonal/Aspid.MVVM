#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget, TProperty}"/> that binds <see cref="GameObject.tag"/>.
    /// </summary>
    /// <remarks>
    /// Unity throws when the tag is not declared in Tags and Layers.
    /// </remarks>
    [Serializable]
    public sealed class GameObjectTagBinder : TargetBinder<GameObject, string>
    {
        /// <param name="target">The object to bind.</param>
        /// <param name="converter">
        /// The converter applied to the bound value, or <see langword="null"/> to use it as-is.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public GameObjectTagBinder(
            GameObject target,
            IConverter<string?, string?>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected override string? Property
        {
            get => Target.tag;
            set => Target.tag = value;
        }
    }
}
