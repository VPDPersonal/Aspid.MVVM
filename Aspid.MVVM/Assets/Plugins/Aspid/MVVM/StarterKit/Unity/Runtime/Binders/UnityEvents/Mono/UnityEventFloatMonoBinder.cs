using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound numeric ViewModel value converted to <see cref="float"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(float))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Float")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Float")]
    public sealed partial class UnityEventFloatMonoBinder : MonoBinder, INumberBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<float> _set;

        /// <summary>
        /// Converts the value to <see cref="float"/> and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <summary>
        /// Converts the value to <see cref="float"/> and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <summary>
        /// Invokes the event with the specified float value, applying the converter if configured.
        /// </summary>
        [BinderLog]
        public void SetValue(float value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);

        /// <summary>
        /// Converts the value to <see cref="float"/> and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}
