#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class TransformPositionBinder : TargetBinder<Transform>, IVectorBinder
    {
        [SerializeField] private Space _space;
        [SerializeField] private Vector3CombineConverter? _converter;

        public TransformPositionBinder(Transform transform, BindMode mode)
            : this(transform, Space.World, null, mode) { }
        
        public TransformPositionBinder(Transform transform, Space space, BindMode mode)
            : this(transform, space, null, mode) { }
        
        public TransformPositionBinder(
            Transform transform,
            Vector3CombineConverter? converter,
            BindMode mode = BindMode.OneWay)
            : this(transform, Space.World, converter, mode) { }
        
        public TransformPositionBinder(
            Transform target,
            Space space = Space.World,
            Vector3CombineConverter? converter = null, 
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _space = space;
            _converter = converter;
        }
        
        public void SetValue(Vector2 value) => 
            SetValue((Vector3)value);
        
        public void SetValue(Vector3 value) =>
            Target.SetPosition(value, _space, _converter);
    }
}