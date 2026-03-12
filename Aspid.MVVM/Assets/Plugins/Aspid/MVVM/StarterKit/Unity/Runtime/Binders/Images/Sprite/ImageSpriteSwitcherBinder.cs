#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{Image, Sprite?}"/> that switches the <see cref="Image.sprite"/> property
    /// between two <see cref="Sprite"/> assets based on the bound boolean ViewModel value.
    /// Optionally disables the <see cref="Image"/> when the selected sprite is <see langword="null"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-Image-Sprite-1.1.0.xml" path="doc//member[@name='ImageSpriteSwitcherBinder']/*" />
    [Serializable]
    public sealed class ImageSpriteSwitcherBinder : SwitcherBinder<Image, Sprite?>
    {
        [Tooltip("When enabled, disables the Image component when the selected sprite is null.")]
        [SerializeField] private bool _disabledWhenNull;

        /// <inheritdoc/>
        public ImageSpriteSwitcherBinder(
            Image target,
            Sprite trueValue,
            Sprite falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, disabledWhenNull: true, mode) { }

        public ImageSpriteSwitcherBinder(
            Image target,
            Sprite trueValue,
            Sprite falseValue,
            bool disabledWhenNull = true,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            _disabledWhenNull = disabledWhenNull;
        }

        /// <summary>
        /// Called when applying the selected value to the <see cref="Image.sprite"/> property.
        /// Disables the <see cref="Image"/> when <paramref name="value"/> is <see langword="null"/> and <c>_disabledWhenNull</c> is <see langword="true"/>.
        /// </summary>
        protected override void SetValue(Sprite? value)
        {
            Target.sprite = value;
            Target.enabled = !_disabledWhenNull || value;
        }
    }
}