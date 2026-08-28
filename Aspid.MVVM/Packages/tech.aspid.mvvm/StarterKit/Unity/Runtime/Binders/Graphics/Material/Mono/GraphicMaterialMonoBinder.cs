using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds the <see cref="Graphic.material"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Material")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Material")]
    public class GraphicMaterialMonoBinder : ComponentMonoBinder<Graphic, Material>
    {
        /// <inheritdoc/>
        protected sealed override Material Property
        {
            get => CachedComponent.material;
            set => CachedComponent.material = value;
        }
    }
}