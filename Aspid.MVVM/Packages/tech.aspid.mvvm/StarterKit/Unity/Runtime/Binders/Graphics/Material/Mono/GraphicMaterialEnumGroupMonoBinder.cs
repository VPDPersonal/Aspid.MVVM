using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinderWithConverter{T1, T2}"/> that sets the <see cref="Graphic.material"/>
    /// property on each <see cref="Graphic"/> in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Material EnumGroup")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Material", SubPath = "EnumGroup")]
    public sealed class GraphicMaterialEnumGroupMonoBinder : EnumGroupMonoBinderWithConverter<Graphic, Material>
    {
        /// <inheritdoc/>
        protected override void SetValue(Graphic element, Material value) =>
            element.material = value;
    }
}