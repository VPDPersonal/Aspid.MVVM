using UnityEngine;
using UnityEngine.Events;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector3>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IBinder{T}"/> that converts a bound <see cref="Vector2"/>
    /// to a <see cref="Vector3"/> using a configurable converter and forwards the result to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// By default, uses <see cref="Vector2Vector3Converter"/> for the conversion.
    /// </remarks>
    [AddBinderContextMenuByType(typeof(Vector3))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/Vector2 To Vector3 Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/Vector2 To Vector3 Caster Binder")]
    public sealed partial class Vector2ToVector3CasterMonoBinder : MonoBinder, IBinder<Vector2>
    {
        [Tooltip("The converter used to transform the bound Vector2 to a Vector3.")]
        [SerializeReference] private Converter _converter = new Vector2Vector3Converter();
        
        [Tooltip("Invoked with the converted Vector3 value each time a new value arrives from the ViewModel.")]
        [SerializeField] private UnityEvent<Vector3> _casted;
        
        /// <summary>
        /// Called by Unity in the Editor when a serialized field value changes.
        /// Assigns the default <see cref="Vector2Vector3Converter"/> if no converter is set.
        /// </summary>
        private void OnValidate() =>
            _converter ??= new Vector2Vector3Converter();
        
        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="Vector3"/> using the configured converter
        /// and invokes the target <see cref="UnityEvent{T}"/>.
        /// </summary>
        /// <param name="value">The <see cref="Vector2"/> value received from the ViewModel.</param>
        /// <remarks>
        /// If no converter is assigned, logs a Unity error and returns without invoking the event.
        /// </remarks>
        [BinderLog]
        public void SetValue(Vector2 value)
        {
            if (_converter is null)
            {
                Debug.LogError($"No converter assigned to {nameof(Vector2ToVector3CasterMonoBinder)}", context: this);
                return;
            }
            
            _casted?.Invoke(_converter.Convert(value));
        }
    }
}