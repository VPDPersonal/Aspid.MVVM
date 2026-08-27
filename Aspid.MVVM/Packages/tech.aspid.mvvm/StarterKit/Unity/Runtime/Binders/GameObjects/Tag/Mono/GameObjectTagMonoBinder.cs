using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that sets the <see cref="GameObject.tag"/> property of the
    /// <see cref="GameObject"/> this component is attached to.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current
    /// <see cref="GameObject.tag"/> value is sent back to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Tag")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Tag")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed partial class GameObjectTagMonoBinder : MonoBinder, 
        IBinder<string>,
        IReverseBinder<string>
    {
        /// <inheritdoc/>
        public event Action<string> ValueChanged;
        
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<string, string> _converter;

        /// <summary>
        /// Sets <see cref="GameObject.tag"/> to <paramref name="value"/> (optionally converted).
        /// </summary>
        /// <param name="value">The string value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(string value) =>
            gameObject.tag = GetConvertedValue(value);
        
        /// <summary>
        /// Called when binding is established. In <see cref="BindMode.OneWayToSource"/>, sends the value the
        /// target already holds to the ViewModel so the source starts in step with the view.
        /// </summary>
        /// <remarks>
        /// Does nothing in the other modes: they push from the ViewModel, and reporting the target's current
        /// value back would be the ViewModel hearing its own state from the view.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedBackValue(gameObject.tag));
        }
        
        private string GetConvertedValue(string value) =>
            _converter?.Convert(value) ?? value;

        /// <summary>
        /// Converts a value on its way back to the ViewModel.
        /// </summary>
        /// <param name="value">The value read from the target.</param>
        /// <returns>
        /// The value as the ViewModel expects it: undone by the converter when it offers
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>, and unchanged when it does not.
        /// </returns>
        /// <remarks>
        /// The forward converter must not be applied here. This binder pushes the target's current
        /// value to the ViewModel, so running it through <c>Convert</c> a second time hands the
        /// ViewModel a value that has been converted twice — visibly wrong for anything that is not
        /// its own inverse.
        /// </remarks>
        private string GetConvertedBackValue(string value) =>
            _converter is ITwoWayConverter<string?, string?> twoWay
                ? twoWay.ConvertBack(value) ?? value
                : value;
    }
}