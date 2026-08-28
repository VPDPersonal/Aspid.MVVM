using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IBinder{T}"/> that converts a bound <see cref="Vector3"/>
    /// to a <see cref="Vector2"/> using a configurable converter and forwards the result to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// By default, uses <see cref="Vector2Vector3Converter"/> for the conversion.
    /// </remarks>
    [AddBinderContextMenuByType(typeof(Vector2))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/Vector3 To Vector2 Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/Vector3 To Vector2 Caster Binder")]
    public sealed partial class Vector3ToVector2CasterMonoBinder : MonoBinder, IBinder<Vector3>
    {
        [Tooltip("The converter used to transform the bound Vector3 to a Vector2.")]
        [SerializeReference] private IConverter<Vector3, Vector2> _converter = new Vector2Vector3Converter();

        [Tooltip("Invoked with the converted Vector2 value.")]
        [SerializeField] private UnityEvent<Vector2> _casted;

        /// <summary>
        /// Called by Unity in the Editor when a serialized field value changes.
        /// Assigns the default <see cref="Vector2Vector3Converter"/> if no converter is set.
        /// </summary>
        private void OnValidate() =>
            _converter ??= new Vector2Vector3Converter();

        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="Vector2"/> using the configured converter
        /// and invokes the target <see cref="UnityEvent{T}"/>.
        /// </summary>
        /// <param name="value">The <see cref="Vector3"/> value received from the ViewModel.</param>
        /// <remarks>
        /// If no converter is assigned, logs a Unity error and returns without invoking the event.
        /// </remarks>
        [BinderLog]
        public void SetValue(Vector3 value)
        {
            if (_converter is null)
            {
                this.LogError("no converter is assigned", "The value is not forwarded.");
                return;
            }
            
            _casted?.Invoke(_converter.Convert(value));
        }
    }
}