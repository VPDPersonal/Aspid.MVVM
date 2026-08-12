using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{MaskableGraphic, Boolean}"/> that sets <see cref="MaskableGraphic.maskable"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(MaskableGraphic), serializePropertyNames: "m_Maskable", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/MaskableGraphic Binder – Maskable Enum")]
    public sealed class GraphicMaskableEnumMonoBinder : EnumMonoBinder<MaskableGraphic, bool>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// </summary>
        protected override void SetValue(bool value) =>
            CachedComponent.maskable = value;
    }
}
