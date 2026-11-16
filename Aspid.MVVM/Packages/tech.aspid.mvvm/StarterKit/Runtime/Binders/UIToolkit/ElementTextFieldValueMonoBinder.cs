#if ASPID_MVVM_HAS_UIELEMENTS
using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{TElement}"/> that binds the text of a <see cref="TextField"/> and reports
    /// user changes back.
    /// </summary>
    /// <remarks>
    /// Writes raise the change event for other listeners; only the binder's own echo is suppressed.
    /// <see langword="null"/> is written as an empty string.
    /// </remarks>
    [BindModeOverride(IsAll = true)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – TextField Value")]
    [AddBinderContextMenuByType(typeof(string))]
    public sealed partial class ElementTextFieldValueMonoBinder : VisualElementMonoBinder<TextField>,
        IBinder<string>,
        IReverseBinder<string>
    {
        private bool _isSuppressingEcho;

        private string _lastValue;
        private bool _hasValue;

        /// <inheritdoc/>
        public event Action<string> ValueChanged;

        /// <summary>
        /// Sets the value without reporting the write back to the ViewModel.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(string value)
        {
            _lastValue = value;
            _hasValue = true;
            var element = ResolveForWrite();
            if (element is null) return;

            _isSuppressingEcho = true;

            try
            {
                element.value = value ?? string.Empty;
            }
            finally
            {
                // Keeps the reverse channel alive when another listener throws.
                _isSuppressingEcho = false;
            }
        }

        /// <inheritdoc/>
        protected override void OnElementAttached(TextField element)
        {
            if (!Mode.IncludesReverse()) return;
            element.UnregisterValueChangedCallback(OnFieldChanged);
            element.RegisterValueChangedCallback(OnFieldChanged);
            if (IsBound && Mode is BindMode.OneWayToSource) OnBound();
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;
            var element = Element;
            if (element is not null)
                ValueChanged?.Invoke(element.value);
        }

        /// <inheritdoc/>
        protected override void OnElementDetached(TextField element) =>
            element.UnregisterValueChangedCallback(OnFieldChanged);

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

        private void OnFieldChanged(ChangeEvent<string> changed)
        {
            if (!_isSuppressingEcho)
                ValueChanged?.Invoke(changed.newValue);
        }
    }
}
#endif
