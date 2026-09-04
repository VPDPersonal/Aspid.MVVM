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
        private bool _isNotifyValueChanged = true;

        /// <inheritdoc/>
        public event Action<string> ValueChanged;

        /// <summary>
        /// Sets the value without reporting the write back to the ViewModel.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(string value)
        {
            var element = Element;
            if (element is null) return;

            _isNotifyValueChanged = false;

            try
            {
                element.value = value ?? string.Empty;
            }
            finally
            {
                // Keeps the reverse channel alive when another listener throws.
                _isNotifyValueChanged = true;
            }
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            var element = Element;
            if (element is null) return;

            element.RegisterValueChangedCallback(OnFieldChanged);
            if (Mode is BindMode.OneWayToSource) ValueChanged?.Invoke(element.value);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            if (Element is not null) Element.UnregisterValueChangedCallback(OnFieldChanged);
            base.OnUnbound();
        }

        private void OnFieldChanged(ChangeEvent<string> changed)
        {
            if (_isNotifyValueChanged)
                ValueChanged?.Invoke(changed.newValue);
        }
    }
}
