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
    /// <see cref="TargetBinder{Graphic, Material, Converter}"/> that sets the <see cref="Graphic.material"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Graphic-Material-1.1.0.xml" path="doc//member[@name='GraphicMaterialBinder']/*" />
    [Serializable]
    public class GraphicMaterialBinder : TargetBinder<Graphic, Material, Converter>
    {
        /// <inheritdoc/>
        protected sealed override Material? Property
        {
            get => Target.material;
            set => Target.material = value;
        }

        /// <inheritdoc/>
        public GraphicMaterialBinder(RawImage target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}