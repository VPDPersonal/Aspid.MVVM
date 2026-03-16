using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<long, long>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterLong;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound numeric ViewModel value converted to <see cref="long"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(long))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Long")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Long")]
    public sealed partial class UnityEventLongMonoBinder : MonoBinder, INumberBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<long> _set;

        /// <summary>
        /// Converts the value to <see cref="long"/> and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((long)value);

        /// <summary>
        /// Invokes the event with the specified long value, applying the converter if configured.
        /// </summary>
        [BinderLog]
        public void SetValue(long value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);

        /// <summary>
        /// Converts the value to <see cref="long"/> and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue((long)value);

        /// <summary>
        /// Converts the value to <see cref="long"/> and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((long)value);
    }
}
