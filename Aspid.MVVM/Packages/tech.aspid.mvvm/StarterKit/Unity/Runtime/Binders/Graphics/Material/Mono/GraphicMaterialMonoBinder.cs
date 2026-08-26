using UnityEngine;
using UnityEngine.UI;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Graphic, Material, Converter}"/> that binds the <see cref="Graphic.material"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current material value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Material")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Material")]
    public class GraphicMaterialMonoBinder : ComponentMonoBinder<Graphic, Material, Converter>
    {
        /// <inheritdoc/>
        protected sealed override Material Property
        {
            get => CachedComponent.material;
            set => CachedComponent.material = value;
        }
    }
}