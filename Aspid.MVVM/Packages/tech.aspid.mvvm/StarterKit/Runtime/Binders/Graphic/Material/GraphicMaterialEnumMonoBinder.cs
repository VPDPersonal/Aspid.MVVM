using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Graphic.material"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Material Enum")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Material", SubPath = "Enum")]
    public sealed class GraphicMaterialEnumMonoBinder : EnumMonoBinder<Graphic, Material>
    {
        /// <inheritdoc/>
        protected override void SetValue(Material value) =>
            CachedComponent.material = value;
    }
}