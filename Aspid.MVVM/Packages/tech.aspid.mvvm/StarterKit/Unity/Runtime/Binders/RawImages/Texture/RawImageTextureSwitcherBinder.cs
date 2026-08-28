#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TTarget,T}"/> that switches the <see cref="RawImage.texture"/>
    /// property between two <see cref="Texture"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-RawImage-Texture-1.1.0.xml" path="doc//member[@name='RawImageTextureSwitcherBinder']/*" />
    [Serializable]
    public sealed class RawImageTextureSwitcherBinder : SwitcherBinder<RawImage, Texture?>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Disables the RawImage component when the selected texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;
        
        /// <param name="target">The <see cref="RawImage"/> to bind.</param>
        /// <param name="trueValue">The <see cref="Texture"/> applied when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The <see cref="Texture"/> applied when the bound value is <see langword="false"/>.</param>
        /// <param name="disabledWhenNull">When <see langword="true"/>, disables the <see cref="RawImage"/> component when the selected texture is <see langword="null"/>.</param>
        /// <param name="converter">The converter used to transform the bound value to a <see cref="Texture"/>.</param>
        /// <param name="mode">The binding mode.</param>
        public RawImageTextureSwitcherBinder(
            RawImage target,
            Texture trueValue,
            Texture falseValue,
            bool disabledWhenNull = true,
            IConverter<Texture?, Texture?>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            _disabledWhenNull = disabledWhenNull;
        }

        /// <summary>
        /// Sets the <see cref="RawImage.texture"/> property to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(Texture? value)
        {
            Target.texture = value;
            if (_disabledWhenNull) Target.enabled = value;
        }
    }
}