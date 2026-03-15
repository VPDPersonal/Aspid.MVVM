using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, bool>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloatToBool;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that converts the bound numeric ViewModel value to a <see langword="bool"/> using a converter and invokes a <see cref="UnityEvent{T}"/> with the result.
    /// </summary>
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Number Condition")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Number Condition")]
    public sealed partial class UnityEventNumberConditionMonoBinder : MonoBinder, INumberBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        [Tooltip("The event invoked with the boolean result of the condition.")]
        [SerializeField] private UnityEvent<bool> _set;

        /// <summary>
        /// Converts the value to <see cref="float"/> and invokes the event with the converted boolean result.
        /// </summary>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to <see cref="float"/> and invokes the event with the converted boolean result.
        /// </summary>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to a <see langword="bool"/> using the configured converter and invokes the event with the result.
        /// </summary>
        [BinderLog]
        public void SetValue(float value)
        {
            if (_converter is null)
            {
                Debug.LogError($"No converter assigned to {nameof(UnityEventNumberConditionMonoBinder)}", context: this);
                return;
            }

            _set.Invoke(_converter.Convert(value));
        }

        /// <summary>
        /// Converts the value to <see cref="float"/> and invokes the event with the converted boolean result.
        /// </summary>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}
