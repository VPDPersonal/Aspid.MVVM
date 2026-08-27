#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{GameObject}"/> that sets the active state of a <see cref="GameObject"/>
    /// via <see cref="GameObject.SetActive"/> when the bound ViewModel value changes.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current
    /// <see cref="GameObject.activeSelf"/> value is sent back to the ViewModel.
    /// Supports an optional converter.
    /// </remarks>
    /// <include file="XmlExampleDoc-GameObject-Visible-1.1.0.xml" path="doc//member[@name='GameObjectVisibleBinder']/*" />
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed class GameObjectVisibleBinder : TargetBinder<GameObject>,
        IBinder<bool>,
        IReverseBinder<bool>
    {
        /// <inheritdoc/>
        public event Action<bool>? ValueChanged;

        [Tooltip("Optional converter applied to the value; runs in reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<bool, bool>? _converter;

        /// <param name="target">The <see cref="GameObject"/> whose active state is bound.</param>
        /// <param name="converter">
        /// An optional converter applied to the value before it is applied. Pass <see langword="null"/> to use the
        /// value unchanged. Runs in reverse only if it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public GameObjectVisibleBinder(GameObject target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _converter = converter;
        }

        /// <summary>
        /// Sets the <see cref="GameObject"/> active state to <paramref name="value"/>, applying the configured
        /// converter if present.
        /// </summary>
        /// <param name="value">The boolean value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            Target.SetActive(_converter?.Convert(value) ?? value);

        /// <summary>
        /// Called when binding is established. In <see cref="BindMode.OneWayToSource"/>, sends the value the
        /// target already holds to the ViewModel so the source starts in step with the view.
        /// </summary>
        /// <remarks>
        /// Does nothing in the other modes: they push from the ViewModel, and reporting the target's current
        /// value back would be the ViewModel hearing its own state from the view. The converter runs in this
        /// direction only when it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>; otherwise the raw
        /// state is sent.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;

            var active = Target.activeSelf;
            ValueChanged?.Invoke(_converter is ITwoWayConverter<bool, bool> twoWay ? twoWay.ConvertBack(active) : active);
        }
    }
}
