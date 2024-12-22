#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class MeshColliderMeshSwitcherBinder : SwitcherBinder<Mesh>
    {
        [Header("Component")]
        [SerializeField] private MeshCollider _collider;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Mesh?, Mesh?>? _converter;

        public MeshColliderMeshSwitcherBinder(
            Mesh trueValue, 
            Mesh falseValue, 
            MeshCollider collider,
            Func<Mesh?, Mesh?> converter)
            : this(trueValue, falseValue, collider, converter.ToConvert()) { }
        
        public MeshColliderMeshSwitcherBinder(
            Mesh trueValue, 
            Mesh falseValue, 
            MeshCollider collider,
            IConverter<Mesh?, Mesh?>? converter = null)
            : base(trueValue, falseValue)
        {
            _converter = converter;
            _collider = collider ?? throw new ArgumentNullException(nameof(collider));
        }

        protected override void SetValue(Mesh value) =>
            _collider.sharedMesh = _converter?.Convert(value) ?? value;
    }
}