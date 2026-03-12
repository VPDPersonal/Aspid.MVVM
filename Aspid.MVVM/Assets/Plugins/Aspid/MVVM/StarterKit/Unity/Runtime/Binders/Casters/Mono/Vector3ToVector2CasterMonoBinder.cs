using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3ToVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IBinder{T}"/> that converts a bound <see cref="Vector3"/>
    /// to a <see cref="Vector2"/> using a configurable converter and forwards the result to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// By default, uses <see cref="Vector3ToVector2Converter"/> for the conversion.
    /// </remarks>
    [AddBinderContextMenuByType(typeof(Vector2))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/Vector3 To Vector2 Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/Vector3 To Vector2 Caster Binder")]
    public sealed partial class Vector3ToVector2CasterMonoBinder : MonoBinder, IBinder<Vector3>
    {
        [Tooltip("The converter used to transform the bound Vector3 to a Vector2.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter = new Vector3ToVector2Converter();
        
        [Tooltip("Invoked with the converted Vector2 value each time a new value arrives from the ViewModel.")]
        [SerializeField] private UnityEvent<Vector2> _casted;
        
        /// <summary>
        /// Called by Unity in the Editor when a serialized field value changes.
        /// Assigns the default <see cref="Vector3ToVector2Converter"/> if no converter is set.
        /// </summary>
        private void OnValidate() =>
            _converter ??= new Vector3ToVector2Converter();
        
        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="Vector2"/> using the configured converter
        /// and invokes the target <see cref="UnityEvent{T}"/>.
        /// </summary>
        /// <param name="value">The <see cref="Vector3"/> value received from the ViewModel.</param>
        /// <remarks>
        /// If no converter is assigned, logs an error via <c>UnityEngine.Debug.LogError</c> and returns
        /// without invoking the event.
        /// </remarks>
        [BinderLog]
        public void SetValue(Vector3 value)
        {
            if (_converter is null)
            {
                Debug.LogError($"No converter assigned to {nameof(Vector3ToVector2CasterMonoBinder)}", context: this);
                return;
            }
            
            _casted?.Invoke(_converter.Convert(value));
        }
    }
}