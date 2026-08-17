#nullable enable
using System;
using UnityEngine.UI;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.UI.ColorBlock, UnityEngine.UI.ColorBlock>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Selectable, ColorBlock, Converter}"/> that sets the <see cref="Selectable.colors"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Selectable-ColorBlock-1.1.0.xml" path="doc//member[@name='SelectableColorBlockBinder']/*" />
    [Serializable]
    public class SelectableColorBlockBinder : TargetBinder<Selectable, ColorBlock, Converter>
    {
        /// <inheritdoc/>
        protected sealed override ColorBlock Property
        {
            get => Target.colors;
            set => Target.colors = value;
        }

        /// <inheritdoc/>
        public SelectableColorBlockBinder(Selectable target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}