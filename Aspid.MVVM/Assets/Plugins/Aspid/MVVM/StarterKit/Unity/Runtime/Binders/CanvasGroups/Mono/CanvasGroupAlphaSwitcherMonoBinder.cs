using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that switches the <see cref="CanvasGroup.alpha"/> property on a <see cref="CanvasGroup"/>
    /// between two values based on a bound boolean ViewModel property.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha Switcher")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", SubPath = "Switcher")]
    public sealed class CanvasGroupAlphaSwitcherMonoBinder : SwitcherMonoBinder<CanvasGroup, float, Converter>
    {
        protected override void SetValue(float value) =>
            CachedComponent.alpha = Mathf.Clamp01(value);
    }
}