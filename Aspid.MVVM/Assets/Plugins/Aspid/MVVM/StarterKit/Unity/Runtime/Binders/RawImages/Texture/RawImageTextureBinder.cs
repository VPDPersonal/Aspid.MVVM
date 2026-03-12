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
    /// <see cref="TargetBinder{RawImage, Texture, Converter}"/> that sets the <see cref="RawImage.texture"/> property,
    /// also accepting <see cref="Sprite"/> values by extracting their underlying texture.
    /// </summary>
    /// <remarks>
    /// Disables the <see cref="RawImage"/> component when the bound texture is <see langword="null"/> and
    /// the <c>disabledWhenNull</c> option is set to <see langword="true"/> (the default).
    /// </remarks>
    /// <include file="XmlExampleDoc-RawImage-Texture-1.1.0.xml" path="doc//member[@name='RawImageTextureBinder']/*" />
    [Serializable]
    public class RawImageTextureBinder : TargetBinder<RawImage, Texture, Converter>, IBinder<Sprite?>
    {
        [Tooltip("When true, disables the RawImage component automatically when the bound texture is null.")]
        [SerializeField] private bool _disabledWhenNull;

        /// <inheritdoc/>
        protected sealed override Texture? Property
        {
            get => Target.texture;
            set
            {
                Target.texture = value;
                Target.enabled = !_disabledWhenNull || value;
            }
        }
        
        /// <inheritdoc/>
        public RawImageTextureBinder(RawImage target, bool disabledWhenNull = true, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _disabledWhenNull = disabledWhenNull;
        }

        /// <inheritdoc/>
        public void SetValue(Sprite? value) =>
            SetValue(value?.texture);
    }
}