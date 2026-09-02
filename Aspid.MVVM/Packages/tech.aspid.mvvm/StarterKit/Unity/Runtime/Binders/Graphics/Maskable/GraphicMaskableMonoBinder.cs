using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds the <see cref="MaskableGraphic.maskable"/> property.
    /// </summary>
    /// <remarks>
    /// Turning this off exempts the graphic from any enclosing mask, so it draws outside the masked area.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(MaskableGraphic), serializePropertyNames: "m_Maskable")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/MaskableGraphic Binder – Maskable")]
    public class GraphicMaskableMonoBinder : ComponentMonoBinder<MaskableGraphic, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.maskable;
            set => CachedComponent.maskable = value;
        }
    }
}
