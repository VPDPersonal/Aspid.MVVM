#if ASPID_MVVM_HAS_UIELEMENTS
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

        private bool _lastValue;
        private bool _hasValue;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(bool value)
        {
            _lastValue = value;
            _hasValue = true;
            var element = ResolveForWrite();
            if (element is null) return;

            var isVisible = _converter?.Convert(value) ?? value;
            element.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
        }
        /// <inheritdoc/>
        protected override void ClearValue()
        {
            _hasValue = false;
            _lastValue = default;
        }

        /// <inheritdoc/>
        protected override void RestoreElement()
        {
            if (_hasValue) SetValue(_lastValue);
        }

    }
}
#endif
