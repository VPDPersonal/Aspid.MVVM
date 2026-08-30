using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{MaskableGraphic, Boolean}"/> that sets <see cref="MaskableGraphic.maskable"/>
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(MaskableGraphic), serializePropertyNames: "m_Maskable", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/MaskableGraphic Binder – Maskable EnumGroup")]
    public sealed class GraphicMaskableEnumGroupMonoBinder : EnumGroupMonoBinder<MaskableGraphic, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(MaskableGraphic element, bool value) =>
            element.maskable = value;
    }
}
