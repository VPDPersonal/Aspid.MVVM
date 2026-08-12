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
        
        [Tooltip("Optional converter applied to the value before it is used. Leave empty to use the value as-is.")]
        [SerializeReference] private Converter _converter;

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
                ValueChanged?.Invoke(GetConvertedValue(gameObject.tag));
        }
        
        private string GetConvertedValue(string value) =>
            _converter?.Convert(value) ?? value;
    }
}