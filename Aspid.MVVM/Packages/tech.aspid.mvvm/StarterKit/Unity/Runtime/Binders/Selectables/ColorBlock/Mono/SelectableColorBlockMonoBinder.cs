using UnityEngine;
using UnityEngine.UI;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.UI.ColorBlock, UnityEngine.UI.ColorBlock>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Selectable, ColorBlock, Converter}"/> that binds the <see cref="Selectable.colors"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – ColorBlock")]
    public class SelectableColorBlockMonoBinder : ComponentMonoBinder<Selectable, ColorBlock, Converter>
    {
        /// <inheritdoc/>
        protected sealed override ColorBlock Property
        {
            get => CachedComponent.colors;
            set => CachedComponent.colors = value;
        }
    }
}