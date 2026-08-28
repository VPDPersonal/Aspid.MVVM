#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;GameObject, bool&gt;</see> that shows or hides the
    /// bound <see cref="GameObject"/> via <see cref="GameObject.SetActive"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-GameObject-Visible-1.1.0.xml" path="doc//member[@name='GameObjectVisibleBinder']/*" />
    [Serializable]
    public sealed class GameObjectVisibleBinder : TargetBinder<GameObject, bool>
    {
        /// <param name="target">The <see cref="GameObject"/> whose active state is bound.</param>
        /// <param name="converter">
        /// An optional converter applied to the value before it is applied. Pass <see langword="null"/> to use the
        /// value unchanged. Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the active state raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public GameObjectVisibleBinder(GameObject target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected override bool Property
        {
            get => Target.activeSelf;
            set => Target.SetActive(value);
        }
    }
}
