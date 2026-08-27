#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinderWithConverter{T1, T2}"/> that sets the <see cref="Selectable.colors"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Selectable-ColorBlock-1.1.0.xml" path="doc//member[@name='SelectableColorBlockBinder']/*" />
    [Serializable]
    public class SelectableColorBlockBinder : TargetBinderWithConverter<Selectable, ColorBlock>
    {
        /// <inheritdoc/>
        protected sealed override ColorBlock Property
        {
            get => Target.colors;
            set => Target.colors = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public SelectableColorBlockBinder(
            Selectable target,
            IConverter<ColorBlock, ColorBlock>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}