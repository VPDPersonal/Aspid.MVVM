using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound ViewModel value converted to <see cref="string"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – String")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – String")]
    public sealed partial class UnityEventStringMonoBinder : MonoBinder, IBinder<string>, IAnyBinder, INumberBinder
    {
        [Tooltip("The culture used when converting numeric and object values to string.")]
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<string> _set;

        /// <summary>
        /// Invokes the event with the specified string value, applying the converter if configured.
        /// </summary>
        [BinderLog]
        public void SetValue(string value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);

        /// <summary>
        /// Converts the value to <see cref="string"/> using the configured culture and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Converts the value to <see cref="string"/> using the configured culture and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Converts the value to <see cref="string"/> using the configured culture and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Converts the value to <see cref="string"/> using the configured culture and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Converts the value to its string representation and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue<T>(T value) =>
            SetValue(value.ToString());
    }
}
