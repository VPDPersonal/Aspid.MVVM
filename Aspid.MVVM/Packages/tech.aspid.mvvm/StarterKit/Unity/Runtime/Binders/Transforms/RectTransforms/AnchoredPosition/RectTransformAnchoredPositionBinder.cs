#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{RectTransform, Vector3}"/> that sets the <see cref="RectTransform.anchoredPosition"/> or
    /// <see cref="RectTransform.anchoredPosition3D"/> property depending on the configured <see cref="Space"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-RectTransform-AnchoredPosition-1.1.0.xml" path="doc//member[@name='RectTransformAnchoredPositionBinder']/*" />
    [Serializable]
    public class RectTransformAnchoredPositionBinder : TargetBinder<RectTransform, Vector3>, IVector3Binder
    {
        [Tooltip("Which property is written: Self → anchoredPosition, World → anchoredPosition3D.")]
        [SerializeField] private Space _space;

        protected sealed override Vector3 Property
        {
            get => Target.GetAnchoredPosition(_space);
            set => Target.SetAnchoredPosition(value, _space);
        }
        
        /// <param name="target">The <see cref="RectTransform"/> to bind.</param>
        /// <param name="space">Determines which property is used: <see cref="Space.Self"/> for <see cref="RectTransform.anchoredPosition"/>, <see cref="Space.World"/> for <see cref="RectTransform.anchoredPosition3D"/>.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Vector3"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
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