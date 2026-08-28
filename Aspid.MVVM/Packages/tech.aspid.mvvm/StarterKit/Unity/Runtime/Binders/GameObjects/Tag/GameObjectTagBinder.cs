#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;GameObject, string&gt;</see> that binds the
    /// <see cref="GameObject.tag"/> property.
    /// </summary>
    /// <remarks>
    /// Unity throws when the tag is not declared in the Tags and Layers settings.
    /// </remarks>
    /// <include file="XmlExampleDoc-GameObject-Tag-1.1.0.xml" path="doc//member[@name='GameObjectTagBinder']/*" />
    [Serializable]
    public sealed class GameObjectTagBinder : TargetBinder<GameObject, string>
    {
        /// <param name="target">The <see cref="GameObject"/> whose <see cref="GameObject.tag"/> property is bound.</param>
        /// <param name="converter">
        /// An optional converter applied to the value before it is applied. Pass <see langword="null"/> to use the
        /// value unchanged. Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the tag raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public GameObjectTagBinder(GameObject target, IConverter<string?, string?>? converter = null, BindMode mode = BindMode.OneWay)
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
