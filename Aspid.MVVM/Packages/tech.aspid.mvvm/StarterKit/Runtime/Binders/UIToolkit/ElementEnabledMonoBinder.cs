#if ASPID_MVVM_HAS_UIELEMENTS
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{TElement}"/> that binds <see cref="VisualElement.SetEnabled"/>.
    /// </summary>
    /// <remarks>
    /// Applies to the whole subtree.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Enabled")]
    [AddBinderContextMenuByType(typeof(bool))]
    public sealed partial class ElementEnabledMonoBinder : VisualElementMonoBinder<VisualElement>, IBinder<bool>
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
            if (element is not null)
                element.SetEnabled(_converter?.Convert(value) ?? value);
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
