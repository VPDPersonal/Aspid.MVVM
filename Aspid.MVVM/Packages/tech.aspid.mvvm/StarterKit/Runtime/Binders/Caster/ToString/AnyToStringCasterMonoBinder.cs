using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IAnyBinder"/> that converts any bound value to a <see cref="string"/>
    /// with a serialized converter and forwards it to a <see cref="UnityEvent{T}"/>. Defaults to <see cref="ValueToStringConverter{T}"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/Any To String Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/Any To String Caster Binder")]
    public sealed partial class AnyToStringCasterMonoBinder : MonoBinder, IAnyBinder
    {
        [Tooltip("Converter from the bound value to the forwarded string.")]
        [SerializeReference] private IConverter<object, string> _converter = new ValueToStringConverter<object>();

        [Tooltip("Invoked with the converted value.")]
        [SerializeField] private UnityEvent<string> _casted;

        private void OnValidate() =>
            _converter ??= new ValueToStringConverter<object>();

        /// <summary>
        /// Converts <paramref name="value"/> and invokes the event. Logs an error and forwards nothing when no converter is set.
        /// </summary>
        /// <typeparam name="T">The runtime type of the incoming value.</typeparam>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue<T>(T value)
        {
            if (_converter is null)
            {
                this.LogError(
                    problem: "no converter is assigned",
                    consequence: "The value is not forwarded.");

                return;
            }

            _casted?.Invoke(_converter.Convert(value));
        }
    }
}
