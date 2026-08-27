using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that shows or hides the <see cref="GameObject"/> this component is attached to
    /// based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current
    /// <see cref="GameObject.activeSelf"/> value is sent back to the ViewModel.
    /// Supports an optional converter.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Visible")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Visible")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed partial class GameObjectVisibleMonoBinder : MonoBinder,
        IBinder<bool>,
        IReverseBinder<bool>
    {
        /// <inheritdoc/>
        public event Action<bool> ValueChanged;

        [Tooltip("Optional converter applied to the value; runs in reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        /// <summary>
        /// Shows or hides the <see cref="GameObject"/> by calling <see cref="GameObject.SetActive"/>
        /// with <paramref name="value"/>, applying the configured converter if present.
        /// </summary>
        /// <param name="value">The boolean value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value) =>
            gameObject.SetActive(_converter?.Convert(value) ?? value);

        /// <summary>
        /// Called when binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, fires <see cref="ValueChanged"/> with the
        /// current <see cref="GameObject.activeSelf"/> value.
        /// </summary>
        /// <remarks>
        /// The converter runs in this direction only when it implements <see cref="ITwoWayConverter{TFrom, TTo}"/>;
        /// otherwise the raw state is sent.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;

            var active = gameObject.activeSelf;
            ValueChanged?.Invoke(_converter is ITwoWayConverter<bool, bool> twoWay ? twoWay.ConvertBack(active) : active);
        }
    }
}
