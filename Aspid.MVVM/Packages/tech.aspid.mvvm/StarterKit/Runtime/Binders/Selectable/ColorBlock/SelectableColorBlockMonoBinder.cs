using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Selectable.colors"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Colors")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Color Block")]
    public class SelectableColorBlockMonoBinder : ComponentMonoBinder<Selectable, ColorBlock>
    {
        /// <inheritdoc/>
        protected sealed override ColorBlock Property
        {
            get => CachedComponent.colors;
            set => CachedComponent.colors = value;
        }
    }
}
