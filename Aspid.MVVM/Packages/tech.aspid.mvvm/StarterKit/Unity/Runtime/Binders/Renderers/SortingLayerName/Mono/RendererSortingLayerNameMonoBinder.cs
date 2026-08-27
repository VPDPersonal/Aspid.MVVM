using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentStringMonoBinder{Renderer}"/> that binds <see cref="Renderer.sortingLayerName"/>.
    /// </summary>
    /// <remarks>
    /// A name no layer has is refused with an error instead of being silently ignored by Unity.
    /// </remarks>
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Sorting Layer Name")]
    public class RendererSortingLayerNameMonoBinder : ComponentStringMonoBinder<Renderer>
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
                    Debug.LogError($"[SortingLayerName] No sorting layer named '{value}'; ignored.", CachedComponent);
                    return;
                }
                
                CachedComponent.sortingLayerName = value;
            }
        }
    }
}
