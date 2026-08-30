using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Scrollbar}"/> that binds <see cref="Scrollbar.size"/>.
    /// </summary>
    /// <remarks>
    /// Clamped to 0..1: Unity clamps it anyway, silently, and a non-finite value would leave the handle with no size at all.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Scrollbar), serializePropertyNames: "m_Size")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Size")]
    public class ScrollbarSizeMonoBinder : ComponentFloatMonoBinder<Scrollbar>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.size;
            set => CachedComponent.size = this.SafeClamp01(value);
        }
    }
}
