#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Texture?, UnityEngine.Texture?>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{RawImage, Texture, Converter}"/> that sets the <see cref="RawImage.texture"/> property,
    /// also accepting <see cref="Sprite"/> values by extracting their underlying texture.
    /// </summary>
    /// <include file="XmlExampleDoc-RawImage-Texture-1.1.0.xml" path="doc//member[@name='RawImageTextureBinder']/*" />
    [Serializable]
    public class RawImageTextureBinder : TargetBinder<RawImage, Texture, Converter>, IBinder<Sprite?>
    {
        [Tooltip("Disables the RawImage component when the bound texture is null.")]
        [SerializeField] private bool _disabledWhenNull;

        /// <inheritdoc/>
        protected sealed override Texture? Property
        {
            get => Target.texture;
            set
            {
                Target.texture = value;
                if (_disabledWhenNull) Target.enabled = value;
            }
        }
        
        /// <param name="target">The <see cref="RawImage"/> to bind.</param>
        /// <param name="disabledWhenNull">When <see langword="true"/>, disables the <see cref="RawImage"/> component when the bound texture is <see langword="null"/>.</param>
        /// <param name="converter">The converter used to transform the bound value to a <see cref="Texture"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
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