using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="CanvasGroup.alpha"/> property on a <see cref="CanvasGroup"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha")]
    public class CanvasGroupAlphaMonoBinder : ComponentFloatMonoBinder<CanvasGroup>
    {
        protected sealed override float Property
        {
            get => CachedComponent.alpha;
            set => CachedComponent.alpha = value;
        }

        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp01(base.GetConvertedValue(value));
    }
}