using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound numeric ViewModel value converted to <see cref="float"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(float))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Float")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Float")]
    public sealed partial class UnityEventFloatMonoBinder : MonoBinder, IFloatBinder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<float, float> _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<float> _set;
        
        /// <summary>
        /// Invokes the event with the specified float value, applying the converter if configured.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
