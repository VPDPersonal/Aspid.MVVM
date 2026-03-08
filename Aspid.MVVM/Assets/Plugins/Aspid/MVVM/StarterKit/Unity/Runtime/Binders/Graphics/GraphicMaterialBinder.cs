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
    /// <summary>
    /// Binder that sets the <see cref="Graphic.material"/> property on a <see cref="Graphic"/>
    /// when the bound ViewModel value changes.
    /// </summary>
    [Serializable]
    public class GraphicMaterialBinder : TargetBinder<Graphic, Material, Converter>
    {
        protected sealed override Material? Property
        {
            get => Target.material;
            set => Target.material = value;
        }

        public GraphicMaterialBinder(RawImage target, BindMode mode = BindMode.OneWay)
            : this(target, converter: null, mode) { }

        public GraphicMaterialBinder(RawImage target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}