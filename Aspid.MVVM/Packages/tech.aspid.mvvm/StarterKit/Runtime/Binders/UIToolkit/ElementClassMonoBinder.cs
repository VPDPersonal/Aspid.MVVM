#if ASPID_MVVM_HAS_UIELEMENTS
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{TElement}"/> that adds or removes one USS class by the bound
    /// <see langword="bool"/>.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – Class")]
    [AddBinderContextMenuByType(typeof(bool))]
    public sealed partial class ElementClassMonoBinder : VisualElementMonoBinder<VisualElement>, IBinder<bool>
    {
        [Tooltip("USS class toggled by the value.")]
        [SerializeField] private string _class;

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

            if (string.IsNullOrWhiteSpace(_class))
            {
                this.LogError(
                    problem: "no USS class is set",
                    consequence: "The element is left unchanged.");

                return;
            }

            element.EnableInClassList(_class, _converter?.Convert(value) ?? value);
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
