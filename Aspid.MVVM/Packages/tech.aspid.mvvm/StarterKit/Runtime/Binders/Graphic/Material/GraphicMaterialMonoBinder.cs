using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> that binds <see cref="Graphic.material"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Material")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Material")]
    public class GraphicMaterialMonoBinder : ComponentObjectMonoBinder<Graphic, Material>
    {
        /// <inheritdoc/>
        protected sealed override Material Property
        {
            get => CachedComponent.material;
            set => CachedComponent.material = value;
        }
    }
}