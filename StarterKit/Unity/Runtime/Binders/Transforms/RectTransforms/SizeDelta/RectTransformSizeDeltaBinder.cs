#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector3Binder{RectTransform}"/> that sets the <see cref="RectTransform.sizeDelta"/>
    /// according to the configured <see cref="SizeDeltaMode"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-RectTransform-SizeDelta-1.1.0.xml" path="doc//member[@name='RectTransformSizeDeltaBinder']/*" />
    [Serializable]
    public class RectTransformSizeDeltaBinder : TargetVector3Binder<RectTransform>
    {
        [Tooltip("Determines which axes of sizeDelta are modified: Width only, Height only, or both (SizeDelta).")]
        [SerializeField] private SizeDeltaMode _sizeMode;

        protected sealed override Vector3 Property
        {
            get => Target.sizeDelta;
            set => Target.SetSizeDelta(value, _sizeMode);
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="RectTransformSizeDeltaBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="RectTransform"/> to bind.</param>
        /// <param name="sizeMode">Determines which axes of <see cref="RectTransform.sizeDelta"/> are modified.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Vector3"/> value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public RectTransformSizeDeltaBinder(
            RectTransform target, 
            SizeDeltaMode sizeMode = SizeDeltaMode.SizeDelta, 
            IConverter<Vector3, Vector3>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _sizeMode = sizeMode;
        }
    }
}