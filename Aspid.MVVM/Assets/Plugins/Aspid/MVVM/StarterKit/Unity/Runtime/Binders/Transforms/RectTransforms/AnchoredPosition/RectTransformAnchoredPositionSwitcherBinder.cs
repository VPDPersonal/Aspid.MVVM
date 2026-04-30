#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherVector3Binder{RectTransform}"/> that switches the <see cref="RectTransform.anchoredPosition"/> or
    /// <see cref="RectTransform.anchoredPosition3D"/> between two <see cref="Vector3"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-RectTransform-AnchoredPosition-1.1.0.xml" path="doc//member[@name='RectTransformAnchoredPositionSwitcherBinder']/*" />
    [Serializable]
    public sealed class RectTransformAnchoredPositionSwitcherBinder : SwitcherVector3Binder<RectTransform>
    {
        [Tooltip("The space that determines which anchored position property is used: Self for anchoredPosition, World for anchoredPosition3D.")]
        [SerializeField] private Space _space;
        
        /// <summary>
        /// Initializes a new instance of <see cref="RectTransformAnchoredPositionSwitcherBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="RectTransform"/> to bind.</param>
        /// <param name="trueValue">The anchored position applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The anchored position applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="space">Determines which property is used: <see cref="Space.Self"/> for <see cref="RectTransform.anchoredPosition"/>, <see cref="Space.World"/> for <see cref="RectTransform.anchoredPosition3D"/>.</param>
        /// <param name="converter">The converter used to transform the selected <see cref="Vector3"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode.</param>
        public RectTransformAnchoredPositionSwitcherBinder(
            RectTransform target,
            Vector3 trueValue,
            Vector3 falseValue,
            Space space = Space.World, 
            IConverter<Vector3, Vector3>? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode)
        {
            _space = space;
        }

        /// <summary>
        /// Called when applying the selected value to the anchored position of the <see cref="RectTransform"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            Target.SetAnchoredPosition(value, _space);
    }
}