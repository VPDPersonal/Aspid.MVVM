#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material?, UnityEngine.Material?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class RawImageMaterialBinder : TargetBinder<RawImage, Material, Converter>
    {
        protected sealed override Material? Property
        {
            get => Target.material;
            set => Target.material = value;
        }
        
        public RawImageMaterialBinder(RawImage target, BindMode mode = BindMode.OneWay)
            : this(target, converter: null, mode) { }
        
        public RawImageMaterialBinder(RawImage target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}