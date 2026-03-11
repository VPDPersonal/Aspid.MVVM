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
    /// <see cref="EnumGroupMonoBinder{CanvasGroup, float}"/> that sets the <see cref="CanvasGroup.alpha"/>
    /// property on each <see cref="CanvasGroup"/> in the group based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="CanvasGroup.alpha"/>.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha EnumGroup")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", SubPath = "EnumGroup")]
    public sealed class CanvasGroupAlphaEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup, float, Converter>
    {
        /// <inheritdoc/>
        protected override void SetValue(CanvasGroup element, float value) =>
            element.alpha = Mathf.Clamp01(value);
    }
}