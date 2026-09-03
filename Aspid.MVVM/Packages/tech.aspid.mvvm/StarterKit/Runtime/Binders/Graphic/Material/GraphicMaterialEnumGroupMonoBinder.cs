using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Graphic.material"/> per group element.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Material EnumGroup")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Material", SubPath = "EnumGroup")]
    public sealed class GraphicMaterialEnumGroupMonoBinder : EnumGroupMonoBinder<Graphic, Material>
    {
        /// <inheritdoc/>
        protected override void SetValue(Graphic element, Material value) =>
            element.material = value;
    }
}