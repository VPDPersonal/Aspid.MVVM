using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{TElement}"/> that shows or hides the element via
    /// <see cref="IStyle.display"/>.
    /// </summary>
    /// <remarks>
    /// A hidden element takes no layout space, unlike one with <see cref="VisualElement.visible"/> off.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Display")]
    [AddBinderContextMenuByType(typeof(bool))]
    public sealed partial class ElementDisplayMonoBinder : VisualElementMonoBinder<VisualElement>, IBinder<bool>
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        /// <inheritdoc/>
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
