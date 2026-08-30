using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds <see cref="Renderer.sortingLayerName"/>.
    /// </summary>
    /// <remarks>
    /// A name no layer has is refused with an error instead of being silently ignored by Unity.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Sorting Layer Name")]
    public class RendererSortingLayerNameMonoBinder : ComponentMonoBinder<Renderer, string>
    {
        /// <inheritdoc/>
        protected sealed override string Property
        {
            get => CachedComponent.sortingLayerName;
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (SortingLayer.NameToID(value) == 0 && value != "Default")
                {
                    this.LogError($"no sorting layer is named {value.Describe()}", "The layer is left unchanged.");
                    return;
                }
                
                CachedComponent.sortingLayerName = value;
            }
        }
    }
}
