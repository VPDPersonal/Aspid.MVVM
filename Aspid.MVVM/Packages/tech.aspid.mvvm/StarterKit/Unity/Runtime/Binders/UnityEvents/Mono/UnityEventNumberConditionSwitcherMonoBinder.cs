using UnityEngine;
using UnityEngine.Events;
using Converter = Aspid.MVVM.StarterKit.IConverter<float, bool>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that converts the bound numeric ViewModel value to a <see langword="bool"/> using a converter and invokes one of two <see cref="UnityEvent"/> instances based on the result.
    /// </summary>
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Number Condition Switcher")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Number Condition Switcher")]
    public sealed partial class UnityEventNumberConditionSwitcherMonoBinder : MonoBinder, INumberBinder
    {
        [Tooltip("Optional converter applied to the value before it is used. Leave empty to use the value as-is.")]
        [SerializeReference] private Converter _converter;

        [Tooltip("The event invoked when the condition evaluates to true.")]
        [SerializeField] private UnityEvent _trueSet;
        [Tooltip("The event invoked when the condition evaluates to false.")]
        [SerializeField] private UnityEvent _falseSet;

        /// <summary>
        /// Converts the value to <see cref="float"/> and invokes the appropriate event based on the converted boolean result.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to <see cref="float"/> and invokes the appropriate event based on the converted boolean result.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to a <see langword="bool"/> using the configured converter and invokes the corresponding event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value)
        {
            if (_converter is null)
            {
                Debug.LogError($"No converter assigned to {nameof(UnityEventNumberConditionSwitcherMonoBinder)}", context: this);
                return;
            }

            if (_converter.Convert(value)) _trueSet?.Invoke();
            else _falseSet?.Invoke();
        }

        /// <summary>
        /// Converts the value to <see cref="float"/> and invokes the appropriate event based on the converted boolean result.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}
