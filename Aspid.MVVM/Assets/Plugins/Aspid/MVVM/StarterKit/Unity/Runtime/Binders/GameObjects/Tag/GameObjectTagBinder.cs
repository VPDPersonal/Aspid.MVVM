#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{GameObject}"/> that sets the <see cref="GameObject.tag"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current
    /// <see cref="GameObject.tag"/> value is sent back to the ViewModel.
    /// </remarks>
    /// <include file="XmlExampleDoc-GameObject-Tag-1.1.0.xml" path="doc//member[@name='GameObjectTagBinder']/*" />
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed class GameObjectTagBinder : TargetBinder<GameObject>,
        IBinder<string>, 
        IReverseBinder<string>
    {
        /// <inheritdoc/>
        public event Action<string?>? ValueChanged;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        /// <summary>
        /// Initializes a new instance of <see cref="GameObjectTagBinder"/> targeting the specified <see cref="GameObject"/>.
        /// </summary>
        /// <param name="target">The <see cref="GameObject"/> whose <see cref="GameObject.tag"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound string value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public GameObjectTagBinder(GameObject target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _converter = converter;
        }

        /// <summary>
        /// Sets the <see cref="GameObject.tag"/> property to <paramref name="value"/> (optionally converted).
        /// </summary>
        /// <param name="value">The string value received from the ViewModel.</param>
        public void SetValue(string? value) =>
            Target.tag = GetConvertedValue(value);
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(Target.tag));
        }
        
        private string GetConvertedValue(string value) =>
            _converter?.Convert(value) ?? value;
    }
}