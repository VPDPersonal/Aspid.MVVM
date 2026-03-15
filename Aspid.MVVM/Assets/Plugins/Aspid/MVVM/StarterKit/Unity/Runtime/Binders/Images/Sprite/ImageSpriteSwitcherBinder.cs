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

        /// <summary>
        /// Initializes a new instance of <see cref="ImageSpriteSwitcherBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Image"/> to bind.</param>
        /// <param name="trueValue">The <see cref="UnityEngine.Sprite"/> applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The <see cref="UnityEngine.Sprite"/> applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="disabledWhenNull">When <see langword="true"/>, the <see cref="Image"/> is disabled when the selected sprite is <see langword="null"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        /// Disables the <see cref="Image"/> when <paramref name="value"/> is <see langword="null"/> and the disable-when-null option is enabled.
        /// </summary>
        protected override void SetValue(Sprite? value)
        {
            Target.sprite = value;
            Target.enabled = !_disabledWhenNull || value;
        }
    }
}