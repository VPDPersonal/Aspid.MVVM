using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{VisualElement}"/> implementing
    /// <see cref="IBinder{T}">IBinder&lt;bool&gt;</see> that shows and hides an element.
    /// </summary>
    /// <remarks>
    /// Writes <see cref="IStyle.display"/> rather than <see cref="VisualElement.visible"/>: a hidden element should take
    /// no space in the layout, which is what <see cref="DisplayStyle.None"/> means and what
    /// <see cref="VisualElement.visible"/> does not do.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Display")]
    [AddBinderContextMenuByType(typeof(bool))]
    public sealed partial class ElementDisplayMonoBinder : VisualElementMonoBinder<VisualElement>, IBinder<bool>
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        /// <summary>
        /// Shows the element when <paramref name="value"/> is <see langword="true"/>, and hides it otherwise.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value)
        {
            var element = Element;
            if (element is null) return;

            var isVisible = _converter?.Convert(value) ?? value;
            element.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
