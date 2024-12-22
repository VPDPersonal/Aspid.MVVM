#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class TransformPositionBinder : Binder, IVectorBinder
    {
        [Header("Component")]
        [SerializeField] private Transform _transform;
        
        [Header("Parameter")]
        [SerializeField] private Space _space;
        
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter? _converter;
        
        public TransformPositionBinder(Transform transform, Vector3CombineConverter? converter)
            : this(transform, Space.World, converter) { }
        
        public TransformPositionBinder(Transform transform, Space space = Space.World, Vector3CombineConverter? converter = null)
        {
            _space = space;
            _converter = converter;
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }
        
        public void SetValue(Vector2 value) => 
            SetValue((Vector3)value);
        
        public void SetValue(Vector3 value) =>
            _transform.SetPosition(value, _space, _converter);
    }
}