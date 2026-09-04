using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="CanvasGroup.alpha"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 1].
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha")]
    public class CanvasGroupAlphaMonoBinder : ComponentFloatMonoBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.alpha;
            set => CachedComponent.alpha = this.SafeClamp01(value);
        }
    }
}
