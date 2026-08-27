using UnityEngine;
using UnityEngine.Events;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound <see cref="Vector3"/> ViewModel value.
    /// </summary>
    [AddBinderContextMenuByType(typeof(Vector3))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Vector3")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Vector3")]
    public sealed partial class UnityEventVector3MonoBinder : MonoBinder, IBinder<Vector3>
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private Converter _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<Vector3> _set;

        /// <summary>
        /// Invokes the event with the specified <see cref="Vector3"/> value, applying the converter if configured.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Vector3 value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
