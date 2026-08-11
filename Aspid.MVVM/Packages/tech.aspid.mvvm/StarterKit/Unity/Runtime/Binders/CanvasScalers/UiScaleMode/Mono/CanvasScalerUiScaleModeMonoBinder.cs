using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;CanvasScaler, CanvasScaler.ScaleMode&gt;</see> that binds
    /// <see cref="CanvasScaler.uiScaleMode"/>.
    /// </summary>
    /// <remarks>
    /// Which rule decides the canvas scale at all: constant pixels, scaling with the screen, or physical
    /// units. A settings screen that offers a UI scale slider has to switch to
    /// <see cref="CanvasScaler.ScaleMode.ConstantPixelSize"/> before the slider means anything.
    /// </remarks>
    [AddBinderContextMenu(typeof(CanvasScaler), serializePropertyNames: "m_UiScaleMode")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasScaler/CanvasScaler Binder – Ui Scale Mode")]
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
