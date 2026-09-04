using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="CanvasScaler.uiScaleMode"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CanvasScaler), serializePropertyNames: "m_UiScaleMode")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasScaler/CanvasScaler Binder – UI Scale Mode")]
    public class CanvasScalerUiScaleModeMonoBinder : ComponentMonoBinder<CanvasScaler, CanvasScaler.ScaleMode>
    {
        /// <inheritdoc/>
        protected sealed override CanvasScaler.ScaleMode Property
        {
            get => CachedComponent.uiScaleMode;
            set => CachedComponent.uiScaleMode = value;
        }
    }
}
