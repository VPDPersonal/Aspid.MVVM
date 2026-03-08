#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that switches the <see cref="UnityEngine.UI.Image.sprite"/> property on an <see cref="UnityEngine.UI.Image"/>
    /// between two sprites based on a bound boolean ViewModel value.
    /// Optionally disables the image when the selected sprite is <see langword="null"/>.
    /// </summary>
    [Serializable]
    public sealed class ImageSpriteSwitcherBinder : SwitcherBinder<Image, Sprite?>
    {
        [SerializeField] private bool _disabledWhenNull;

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

        protected override void SetValue(Sprite? value)
        {
            Target.sprite = value;
            Target.enabled = !_disabledWhenNull || value;
        }
    }
}