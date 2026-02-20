using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
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