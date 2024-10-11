#nullable enable
using System;
using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.RectTransforms
{
    public class RectTransformAnchoredPositionBinder : Binder, IVectorBinder
    {
        private readonly RectTransform _transform;
        private readonly IConverter<Vector3, Vector3>? _converter;
        
        public RectTransformAnchoredPositionBinder(RectTransform transform, Func<Vector3, Vector3> converter)
            : this(transform, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public RectTransformAnchoredPositionBinder(RectTransform transform, IConverter<Vector3, Vector3>? converter = null)
        {
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public void SetValue(Vector2 value) =>
            _transform.anchoredPosition = _converter?.Convert(value) ?? value;

        public void SetValue(Vector3 value) =>
            _transform.anchoredPosition3D = _converter?.Convert(value) ?? value;
    }

}