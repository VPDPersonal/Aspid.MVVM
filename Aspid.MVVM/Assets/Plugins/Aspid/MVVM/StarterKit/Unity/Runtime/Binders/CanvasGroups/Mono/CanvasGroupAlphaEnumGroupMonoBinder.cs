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
    /// MonoBehaviour binder that sets the <see cref="CanvasGroup.alpha"/> property on a group of <see cref="CanvasGroup"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha EnumGroup")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", SubPath = "EnumGroup")]
    public sealed class CanvasGroupAlphaEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup, float, Converter>
    {
        protected override void SetValue(CanvasGroup element, float value) =>
            element.alpha = Mathf.Clamp01(value);
    }
}