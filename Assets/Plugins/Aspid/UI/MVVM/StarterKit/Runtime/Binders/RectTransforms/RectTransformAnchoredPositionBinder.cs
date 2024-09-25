using System;
using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.RectTransforms
{
    public class RectTransformAnchoredPositionBinder : Binder, IVectorBinder
    {
        protected readonly RectTransform Transform;
        protected readonly IConverter<Vector3, Vector3> Converter;
        
        public RectTransformAnchoredPositionBinder(RectTransform transform, Func<Vector3, Vector3> converter)
            : this(transform, new GenericFuncConverter<Vector3, Vector3>(converter)) { }
        
        public RectTransformAnchoredPositionBinder(RectTransform transform, IConverter<Vector3, Vector3> converter = null)
        {
            Transform = transform;
            Converter = converter;
        }

        public void SetValue(Vector2 value) =>
            Transform.anchoredPosition = Converter?.Convert(value) ?? value;

        public void SetValue(Vector3 value) =>
            Transform.anchoredPosition3D = Converter?.Convert(value) ?? value;
    }

}