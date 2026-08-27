using UnityEngine;
using UnityEngine.Events;
using Converter = Aspid.MVVM.StarterKit.IConverter<object, string>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IAnyBinder"/> that converts any bound value
    /// to a <see cref="string"/> using a configurable converter before forwarding it to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// By default, uses <see cref="GenericToStringConverter{T}"/> for the conversion.
    /// A custom <see cref="Converter"/> can be supplied for specialized formatting.
    /// </remarks>
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/Any To String Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/Any To String Caster Binder")]
    public sealed class AnyToStringCasterMonoBinder : MonoBinder, IAnyBinder
    {
        [Tooltip("The converter used to transform any incoming value to a string.")]
        [SerializeReference] private Converter _converter = new GenericToStringConverter<object>();

        [Tooltip("Invoked with the converted string value.")]
        [SerializeField] private UnityEvent<string> _casted;

        /// <summary>
        /// Called by Unity in the Editor when a serialized field value changes.
        /// Assigns the default <see cref="GenericToStringConverter{T}"/> if no converter is set.
        /// </summary>
        private void OnValidate() =>
            _converter ??= new GenericToStringConverter<object>();

        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="string"/> using the configured converter
        /// and invokes the target <see cref="UnityEvent{T}"/>.
        /// </summary>
        /// <typeparam name="T">The runtime type of the incoming value.</typeparam>
        /// <param name="value">The value received from the ViewModel.</param>
        /// <remarks>
        /// If no converter is assigned, logs a Unity error and returns without invoking the event.
        /// </remarks>
        [BinderLog]
        public void SetValue<T>(T value)
        {
            if (_converter is null)
            {
                Debug.LogError($"No converter assigned to {nameof(AnyToStringCasterMonoBinder)}", context: this);
                return;
            }

            _casted?.Invoke(_converter.Convert(value));
        }
    }
}