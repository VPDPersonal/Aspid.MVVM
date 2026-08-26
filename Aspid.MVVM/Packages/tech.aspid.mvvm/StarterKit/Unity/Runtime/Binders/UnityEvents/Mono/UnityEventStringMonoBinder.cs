using UnityEngine;
using UnityEngine.Events;
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;

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

        [Tooltip("Optional converter applied to the value before it is used. Leave empty to use the value as-is.")]
        [SerializeReference] private Converter _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<string> _set;

        /// <summary>
        /// Invokes the event with the specified string value, applying the converter if configured.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(string value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);

        /// <summary>
        /// Converts the value to <see cref="string"/> using the configured culture and invokes the event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Converts the value to <see cref="string"/> using the configured culture and invokes the event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Converts the value to <see cref="string"/> using the configured culture and invokes the event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Converts the value to <see cref="string"/> using the configured culture and invokes the event.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));

        /// <summary>
        /// Converts the value to its string representation and invokes the event.
        /// A <see langword="null"/> value is forwarded as an empty string.
        /// </summary>
        /// <remarks>
        /// This overload is the <see cref="IAnyBinder"/> path, chosen whenever the bound member's type has no
        /// dedicated overload above — that is, for every reference type other than <see cref="string"/>. A bindable
        /// member of such a type starts out <see langword="null"/> and publishes that value the moment the binder
        /// is added, so <see langword="null"/> is the first thing this method sees rather than an edge case.
        /// </remarks>
        [BinderLog]
        public void SetValue<T>(T value) =>
            SetValue(value?.ToString() ?? string.Empty);
    }
}
