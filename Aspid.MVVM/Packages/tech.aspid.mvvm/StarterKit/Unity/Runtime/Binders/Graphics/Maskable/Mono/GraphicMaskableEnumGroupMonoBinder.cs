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
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// </summary>
        /// <param name="element">The component this entry of the group writes to.</param>
        /// <param name="value">The value the bound enum resolved to for this element.</param>
        protected override void SetValue(MaskableGraphic element, bool value) =>
            element.maskable = value;
    }
}
