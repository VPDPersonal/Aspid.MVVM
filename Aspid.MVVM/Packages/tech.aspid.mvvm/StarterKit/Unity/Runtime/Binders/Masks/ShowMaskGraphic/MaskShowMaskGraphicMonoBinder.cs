using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds <see cref="Mask.showMaskGraphic"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Mask), serializePropertyNames: "m_ShowMaskGraphic")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Mask/Mask Binder – Show Mask Graphic")]
    public class MaskShowMaskGraphicMonoBinder : ComponentMonoBinder<Mask, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.showMaskGraphic;
            set => CachedComponent.showMaskGraphic = value;
        }
    }
}
