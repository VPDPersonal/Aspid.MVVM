#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector3Binder{RectTransform}"/> that sets the <see cref="RectTransform.anchoredPosition"/> or
    /// <see cref="RectTransform.anchoredPosition3D"/> property depending on the configured <see cref="Space"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-RectTransform-AnchoredPosition-1.1.0.xml" path="doc//member[@name='RectTransformAnchoredPositionBinder']/*" />
    [Serializable]
    public class RectTransformAnchoredPositionBinder : TargetVector3Binder<RectTransform>
    {
        [Tooltip("The space that determines which anchored position property is used: Self for anchoredPosition, World for anchoredPosition3D.")]
        [SerializeField] private Space _space;

        protected sealed override Vector3 Property
        {
            get => Target.GetAnchoredPosition(_space);
            set => Target.SetAnchoredPosition(value, _space);
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="RectTransformAnchoredPositionBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="RectTransform"/> to bind.</param>
        /// <param name="space">Determines which property is used: <see cref="Space.Self"/> for <see cref="RectTransform.anchoredPosition"/>, <see cref="Space.World"/> for <see cref="RectTransform.anchoredPosition3D"/>.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Vector3"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public RectTransformAnchoredPositionBinder(
            RectTransform target,
            Space space = Space.World, 
            IConverter<Vector3, Vector3>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _space = space;
        }
    }
}