using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

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
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        /// <summary>
        /// Sets <see cref="GameObject.tag"/> to <paramref name="value"/> (optionally converted).
        /// </summary>
        /// <param name="value">The string value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(string value) =>
            gameObject.tag = GetConvertedValue(value);
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(gameObject.tag));
        }
        
        private string GetConvertedValue(string value) =>
            _converter?.Convert(value) ?? value;
    }
}