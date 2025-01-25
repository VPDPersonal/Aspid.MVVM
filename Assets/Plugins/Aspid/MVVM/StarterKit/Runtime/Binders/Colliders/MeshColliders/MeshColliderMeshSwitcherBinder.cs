#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Mesh?, UnityEngine.Mesh?>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterMesh;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class MeshColliderMeshSwitcherBinder : SwitcherBinder<MeshCollider, Mesh>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

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
            Converter? converter = null)
            : base(target, trueValue, falseValue)
        {
            _converter = converter; }

        protected override void SetValue(Mesh value) =>
            Target.sharedMesh = _converter?.Convert(value) ?? value;
    }
}