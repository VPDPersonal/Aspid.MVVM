using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Mask}"/> that binds <see cref="Mask.showMaskGraphic"/>.
    /// </summary>
    /// <remarks>
    /// Whether the graphic that defines the mask is drawn as well as used. Turning it on and off is the
    /// difference between a frame around an avatar and an invisible cut-out, and it was the one property of the
    /// component worth binding.
    /// </remarks>
    [AddBinderContextMenu(typeof(Mask), serializePropertyNames: "m_ShowMaskGraphic")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Mask/Mask Binder – Show Mask Graphic")]
    public class MaskShowMaskGraphicMonoBinder : ComponentBoolMonoBinder<Mask>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.showMaskGraphic;
            set => CachedComponent.showMaskGraphic = value;
        }
    }
}
