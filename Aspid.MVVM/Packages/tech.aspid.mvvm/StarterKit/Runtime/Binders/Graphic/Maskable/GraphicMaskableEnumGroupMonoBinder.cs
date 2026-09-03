using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="MaskableGraphic.maskable"/> per group element.
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
