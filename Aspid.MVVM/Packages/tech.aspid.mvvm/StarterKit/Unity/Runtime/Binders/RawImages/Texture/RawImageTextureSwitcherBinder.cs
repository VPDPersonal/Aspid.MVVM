#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Texture?, UnityEngine.Texture?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterTexture;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{RawImage, Texture, Converter}"/> that switches the <see cref="RawImage.texture"/>
    /// property between two <see cref="Texture"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// Disables the <see cref="RawImage"/> component when the selected texture is <see langword="null"/> and
    /// the disabledWhenNull option is set to <see langword="true"/> (the default).
    /// </remarks>
    /// <include file="XmlExampleDoc-RawImage-Texture-1.1.0.xml" path="doc//member[@name='RawImageTextureSwitcherBinder']/*" />
    [Serializable]
    public sealed class RawImageTextureSwitcherBinder : SwitcherBinder<RawImage, Texture?, Converter>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("When true, disables the RawImage component automatically when the selected texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        /// <inheritdoc/>
        public RawImageTextureSwitcherBinder(
            RawImage target,
            Texture trueValue,
            Texture falseValue,
            bool disabledWhenNull = true,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            _disabledWhenNull = disabledWhenNull;
        }

        /// <summary>
        /// Applies the selected value to the <see cref="RawImage.texture"/> property.
        /// Disables the <see cref="RawImage"/> component when the texture is <see langword="null"/> and the Disable When Null option is enabled.
        /// </summary>
        protected override void SetValue(Texture? value)
        {
            Target.texture = value;
            Target.enabled = !_disabledWhenNull || value;
        }
    }
}