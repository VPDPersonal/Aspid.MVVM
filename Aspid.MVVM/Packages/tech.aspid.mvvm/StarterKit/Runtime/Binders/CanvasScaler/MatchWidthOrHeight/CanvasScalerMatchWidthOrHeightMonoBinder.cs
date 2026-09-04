using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="CanvasScaler.matchWidthOrHeight"/>.
    /// </summary>
    /// <remarks>
    /// Applies only in <see cref="CanvasScaler.ScreenMatchMode.MatchWidthOrHeight"/>. The value is clamped to [0, 1].
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CanvasScaler), serializePropertyNames: "m_MatchWidthOrHeight")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasScaler/CanvasScaler Binder – Match Width Or Height")]
    public class CanvasScalerMatchWidthOrHeightMonoBinder : ComponentFloatMonoBinder<CanvasScaler>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.matchWidthOrHeight;
            set => CachedComponent.matchWidthOrHeight = this.SafeClamp01(value);
        }
    }
}
