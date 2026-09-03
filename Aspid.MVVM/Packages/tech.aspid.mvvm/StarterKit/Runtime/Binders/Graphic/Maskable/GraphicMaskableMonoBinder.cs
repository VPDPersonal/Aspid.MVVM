using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="MaskableGraphic.maskable"/>.
    /// </summary>
    /// <remarks>
    /// Off, the graphic ignores any enclosing mask and draws outside the masked area.
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
