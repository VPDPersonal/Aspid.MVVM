using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<double, double>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterDouble;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound numeric ViewModel value converted to <see cref="double"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(double))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Double")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Double")]
    public sealed partial class UnityEventDoubleMonoBinder : MonoBinder, INumberBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<double> _set;

        /// <summary>
        /// Converts the value to <see cref="double"/> and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((double)value);

        /// <summary>
        /// Converts the value to <see cref="double"/> and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((double)value);

        /// <summary>
        /// Converts the value to <see cref="double"/> and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue((double)value);

        /// <summary>
        /// Invokes the event with the specified double value, applying the converter if configured.
        /// </summary>
        [BinderLog]
        public void SetValue(double value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
