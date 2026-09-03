using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="MaskableGraphic.maskable"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(MaskableGraphic), serializePropertyNames: "m_Maskable", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/MaskableGraphic Binder – Maskable Enum")]
    public sealed class GraphicMaskableEnumMonoBinder : EnumMonoBinder<MaskableGraphic, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.maskable = value;
    }
}
