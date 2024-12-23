#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class MeshColliderMeshSwitcherBinder : SwitcherBinder<MeshCollider, Mesh>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Mesh?, Mesh?>? _converter;

        public MeshColliderMeshSwitcherBinder(
            MeshCollider target,
            Mesh trueValue, 
            Mesh falseValue, 
            Func<Mesh?, Mesh?> converter)
            : this(target, trueValue, falseValue, converter.ToConvert()) { }
        
        public MeshColliderMeshSwitcherBinder(
            MeshCollider target,
            Mesh trueValue, 
            Mesh falseValue, 
            IConverter<Mesh?, Mesh?>? converter = null)
            : base(target, trueValue, falseValue)
        {
            _converter = converter; }

        protected override void SetValue(Mesh value) =>
            Target.sharedMesh = _converter?.Convert(value) ?? value;
    }
}