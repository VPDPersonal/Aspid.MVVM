using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, bool>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterStringToBool;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IBinder{T}"/> that converts a bound <see cref="string"/>
    /// to a <see cref="bool"/> using a configurable converter and forwards the result to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// By default, uses <see cref="StringEmptyToBoolConverter"/> for the conversion.
    /// </remarks>
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/String To Bool Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/String To Bool Caster Binder")]
    public sealed partial class StringToBoolCasterMonoBinder : MonoBinder, IBinder<string>
    {
        [Tooltip("The converter used to transform the bound string to a bool.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter = new StringEmptyToBoolConverter();
        
        [Tooltip("Invoked with the converted bool value each time a new value arrives from the ViewModel.")]
        [SerializeField] private UnityEvent<bool> _casted;
        
        /// <summary>
        /// Called by Unity in the Editor when a serialized field value changes.
        /// Assigns the default <see cref="StringEmptyToBoolConverter"/> if no converter is set.
        /// </summary>
        private void OnValidate() =>
            _converter ??= new StringEmptyToBoolConverter();
        
        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="bool"/> using the configured converter
        /// and invokes the target <see cref="UnityEvent{T}"/>.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value received from the ViewModel.</param>
        /// <remarks>
        /// If no converter is assigned, logs a Unity error and returns without invoking the event.
        /// </remarks>
        [BinderLog]
        public void SetValue(string value)
        {
            if (_converter is null)
            {
                Debug.LogError($"No converter assigned to {nameof(StringToBoolCasterMonoBinder)}", context: this);
                return;
            }
            
            _casted?.Invoke(_converter.Convert(value));
        }
    }
}