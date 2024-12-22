#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class RectTransformAnchoredPositionSwitcherBinder : SwitcherBinder<Vector3>
    {
        [Header("Parameters")]
        [SerializeField] private Space _space;
        
        [Header("Components")]
        [SerializeField] private RectTransform _transform;
        
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter;

        public RectTransformAnchoredPositionSwitcherBinder(
            Vector3 trueValue,
            Vector3 falseValue,
            RectTransform transform,
            Vector3CombineConverter? converter) 
            : this(trueValue, falseValue, transform, Space.World, converter) { }
        
        public RectTransformAnchoredPositionSwitcherBinder(
            Vector3 trueValue,
            Vector3 falseValue,
            RectTransform transform,
            Space space = Space.World, 
            Vector3CombineConverter? converter = null) 
            : base(trueValue, falseValue)
        {
            _space = space;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        protected override void SetValue(Vector3 value) =>
            _transform.SetAnchoredPosition(value, _space, _converter);
    }
}