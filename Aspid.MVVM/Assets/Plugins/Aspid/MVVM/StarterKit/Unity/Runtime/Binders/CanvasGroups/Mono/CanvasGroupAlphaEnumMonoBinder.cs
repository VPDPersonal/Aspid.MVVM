using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha Enum")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", SubPath = "Enum")]
    public sealed class CanvasGroupAlphaEnumMonoBinder : EnumMonoBinder<CanvasGroup, float, Converter>
    {
        protected override void SetValue(float value) =>
            CachedComponent.alpha = Mathf.Clamp01(value);
    }
}