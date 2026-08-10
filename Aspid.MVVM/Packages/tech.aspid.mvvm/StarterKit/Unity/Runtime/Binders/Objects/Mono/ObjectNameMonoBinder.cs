using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that sets the <see cref="Object.name"/> property of a target <see cref="Object"/>
    /// when the bound ViewModel value changes.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current name
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Object/Object Binder – Name")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Object/Object Binder – Name")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed partial class ObjectNameMonoBinder : MonoBinder,
        IBinder<string>,
        IReverseBinder<string>
    {
        /// <summary>
        /// Raised when the bound value changes.
        /// </summary>
        public event Action<string> ValueChanged;
        
        [Tooltip("The target Object whose name property will be driven by the binding.")]
        [SerializeField] private Object _object;
        
        [Tooltip("Optional converter applied to the string value before it is set on the target or sent back to the ViewModel.")]
        [SerializeReference] private Converter _converter;
        
        private void OnValidate()
        {
            if (!_object)
                _object = gameObject;
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(string value) =>
            _object.name = GetConvertedValue(value);

        /// <summary>
        /// Called after binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, propagates the current <see cref="Object.name"/> to the ViewModel.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedBackValue(_object.name));
        }
        
        private string GetConvertedValue(string value) =>
            _converter?.Convert(value) ?? value ?? string.Empty;

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