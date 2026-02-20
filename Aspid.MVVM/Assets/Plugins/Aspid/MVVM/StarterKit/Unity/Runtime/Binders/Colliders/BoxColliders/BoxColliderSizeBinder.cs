#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class BoxColliderSizeBinder : TargetVector3Binder<BoxCollider>
    {
        protected sealed override Vector3 Property
        {
            get => Target.size;
            set => Target.size = value;
        }
        
        public BoxColliderSizeBinder(BoxCollider target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public BoxColliderSizeBinder(
            BoxCollider target, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}