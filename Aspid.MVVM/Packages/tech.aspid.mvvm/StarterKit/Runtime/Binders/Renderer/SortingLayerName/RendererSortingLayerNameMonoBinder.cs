using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Renderer.sortingLayerName"/>.
    /// </summary>
    /// <remarks>
    /// An empty name selects the Default layer; an unknown name is reported and leaves the layer unchanged.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_SortingLayerID")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Sorting Layer Name")]
    public class RendererSortingLayerNameMonoBinder : ComponentMonoBinder<Renderer, string>
    {
        private const string DefaultLayer = "Default";

        /// <inheritdoc/>
        protected sealed override string Property
        {
            get => CachedComponent.sortingLayerName;
            set
            {
                if (string.IsNullOrEmpty(value)) value = DefaultLayer;

                if (value != DefaultLayer && SortingLayer.NameToID(value) == 0)
                {
                    this.LogError(
                        problem: $"no sorting layer is named {value.Describe()}",
                        consequence: "The layer is left unchanged.");

                    return;
                }

                CachedComponent.sortingLayerName = value;
            }
        }
    }
}
