using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentStringMonoBinder{Renderer}"/> that binds <see cref="Renderer.sortingLayerName"/>.
    /// </summary>
    /// <remarks>
    /// Which sorting layer the renderer belongs to. A name no layer has is refused with an error: Unity ignores
    /// it silently and leaves the object where it was, which looks exactly like a depth bug.
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
                // Unity молча игнорирует имя несуществующего слоя, поэтому существование проверяется заранее:
                // иначе опечатка оставляет объект на прежнем слое и ничего об этом не сообщает.
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
