using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{VisualElement}"/> implementing
    /// <see cref="IBinder{T}">IBinder&lt;bool&gt;</see> that enables and disables an element.
    /// </summary>
    /// <remarks>
    /// <see cref="VisualElement.SetEnabled"/> greys the element out and stops it receiving input, and it applies to the
    /// whole subtree — which is how a panel of controls is disabled as one.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Enabled")]
    [AddBinderContextMenuByType(typeof(bool))]
    public sealed partial class ElementEnabledMonoBinder : VisualElementMonoBinder<VisualElement>, IBinder<bool>
    {
        [Tooltip("When enabled, the bound value is inverted before it is applied — bind an IsBusy flag to it directly.")]
        [SerializeField] private bool _isInvert;

        /// <summary>
        /// Enables the element when <paramref name="value"/> is <see langword="true"/>, and disables it otherwise.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value)
        {
            var element = Element;
            if (element is null) return;

            element.SetEnabled(_isInvert ? !value : value);
        }
    }
}
