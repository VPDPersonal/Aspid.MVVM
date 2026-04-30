#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherVector3Binder{RectTransform}"/> that switches the <see cref="RectTransform.sizeDelta"/>
    /// between two <see cref="Vector2"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-RectTransform-SizeDelta-1.1.0.xml" path="doc//member[@name='RectTransformSizeDeltaSwitcherBinder']/*" />
    [Serializable]
    public sealed class RectTransformSizeDeltaSwitcherBinder : SwitcherVector3Binder<RectTransform>
    {
        [Tooltip("Determines which axes of sizeDelta are modified: Width only, Height only, or both (SizeDelta).")]
        [SerializeField] private SizeDeltaMode _sizeMode;
        
        /// <summary>
        /// Initializes a new instance of <see cref="RectTransformSizeDeltaSwitcherBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="RectTransform"/> to bind.</param>
        /// <param name="trueValue">The size delta applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The size delta applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="sizeMode">Determines which axes of <see cref="RectTransform.sizeDelta"/> are modified.</param>
        /// <param name="converter">The converter used to transform the selected value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode.</param>
        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SizeDeltaMode sizeMode = SizeDeltaMode.SizeDelta,
            IConverter<Vector3, Vector3>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            _sizeMode = sizeMode;
        }

        /// <summary>
        /// Called when applying the selected value to the <see cref="RectTransform.sizeDelta"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            Target.SetSizeDelta(value, _sizeMode);
    }
}