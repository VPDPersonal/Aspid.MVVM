using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinderWithConverter{T1, T2}"/> that binds the <see cref="Selectable.colors"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – ColorBlock")]
    public class SelectableColorBlockMonoBinder : ComponentMonoBinderWithConverter<Selectable, ColorBlock>
    {
        /// <inheritdoc/>
        protected sealed override ColorBlock Property
        {
            get => CachedComponent.colors;
            set => CachedComponent.colors = value;
        }
    }
}