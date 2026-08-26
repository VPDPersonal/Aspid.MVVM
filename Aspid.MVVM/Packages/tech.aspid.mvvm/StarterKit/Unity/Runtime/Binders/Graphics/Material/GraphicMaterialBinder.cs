#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material?, UnityEngine.Material?>;

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

        /// <summary>
        /// Initializes a new instance of <see cref="GraphicMaterialBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Graphic"/> to bind.</param>
        /// <param name="converter">The converter used to transform the bound <see cref="Material"/> value. Pass <see langword="null"/> to use the value unchanged.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public GraphicMaterialBinder(Graphic target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}