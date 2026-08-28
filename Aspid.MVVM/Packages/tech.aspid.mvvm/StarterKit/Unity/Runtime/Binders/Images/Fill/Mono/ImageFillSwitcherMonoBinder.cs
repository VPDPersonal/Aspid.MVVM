using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent,T}">SwitcherMonoBinder&lt;Image, float&gt;</see> that switches the <see cref="Image.fillAmount"/> property
    /// between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The applied value is clamped to [0, 1] before being applied to <see cref="Image.fillAmount"/>.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill Switcher")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount", SubPath = "Switcher")]
    public sealed class ImageFillSwitcherMonoBinder : SwitcherMonoBinder<Image, float>
    {
        /// <summary>
        /// Sets <see cref="Image.fillAmount"/> to <paramref name="value"/>, clamped to 0..1.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            CachedComponent.fillAmount = this.SafeClamp01(value);
    }
}