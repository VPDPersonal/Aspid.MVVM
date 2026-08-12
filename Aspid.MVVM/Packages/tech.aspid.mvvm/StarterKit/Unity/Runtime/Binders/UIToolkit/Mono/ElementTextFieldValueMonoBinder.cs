using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{TextField}"/> implementing
    /// <see cref="IBinder{T}">IBinder&lt;string&gt;</see> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;string&gt;</see> that binds the field's text.
    /// </summary>
    /// <remarks>
    /// Two-way, like its uGUI counterpart: the ViewModel fills the field and what the user types reaches the ViewModel.
    /// <para/>
    /// A write by the binder is not read back as typing — UI Toolkit raises the same change event for both — and the guard
    /// is released in a <see langword="finally"/>, because an exception from another listener would otherwise leave the
    /// reverse channel dead for good.
    /// <para/>
    /// A <see langword="null"/> value is written as an empty string, which is what the field would show anyway; writing
    /// <see langword="null"/> itself makes UI Toolkit throw.
    /// </remarks>
    [BindModeOverride(IsAll = true)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – TextField Value")]
    [AddBinderContextMenuByType(typeof(string))]
    public sealed partial class ElementTextFieldValueMonoBinder : VisualElementMonoBinder<TextField>, IBinder<string>, IReverseBinder<string>
    {
        /// <inheritdoc/>
        public event Action<string> ValueChanged;

        private bool _isNotifying = true;

        /// <summary>
        /// Sets the field's text without reading the write back as typing.
        /// </summary>
        /// <param name="value">The value received from the ViewModel, written as an empty string when <see langword="null"/>.</param>
        [BinderLog]
        public void SetValue(string value)
        {
            var element = Element;
            if (element is null) return;

            _isNotifying = false;

            try
            {
                element.value = value ?? string.Empty;
            }
            finally
            {
                _isNotifying = true;
            }
        }

        /// <summary>
        /// Called when the binder is bound. Listens to the field when the mode carries values back, and sends the current
        /// text once in <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;

            var element = Element;
            if (element is null) return;

            element.RegisterValueChangedCallback(OnFieldChanged);
            if (Mode is BindMode.OneWayToSource) ValueChanged?.Invoke(element.value);
        }

        /// <summary>
        /// Called when the binder is unbound. Stops listening to the field.
        /// </summary>
        protected override void OnUnbound()
        {
            if (Element is not null) Element.UnregisterValueChangedCallback(OnFieldChanged);
            base.OnUnbound();
        }

        private void OnFieldChanged(ChangeEvent<string> changed)
        {
            if (!_isNotifying) return;
            ValueChanged?.Invoke(changed.newValue);
        }
    }
}
