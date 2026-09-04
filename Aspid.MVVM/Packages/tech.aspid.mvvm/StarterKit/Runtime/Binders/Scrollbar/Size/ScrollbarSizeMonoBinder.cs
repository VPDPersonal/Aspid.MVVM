using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="Scrollbar.size"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 1].
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
