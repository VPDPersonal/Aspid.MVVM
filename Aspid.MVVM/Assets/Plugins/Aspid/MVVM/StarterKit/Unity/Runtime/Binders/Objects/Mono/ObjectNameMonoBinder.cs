using System;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

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
        [SerializeReferenceDropdown]
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
                ValueChanged?.Invoke(GetConvertedValue(_object.name));
        }
        
        private string GetConvertedValue(string value) =>
            _converter?.Convert(value) ?? value ?? string.Empty;
    }
}