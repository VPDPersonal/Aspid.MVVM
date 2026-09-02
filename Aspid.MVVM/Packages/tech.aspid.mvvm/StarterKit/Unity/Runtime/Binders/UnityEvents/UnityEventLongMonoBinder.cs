using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound numeric ViewModel value converted to <see cref="long"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(long))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Long")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Long")]
    public sealed partial class UnityEventLongMonoBinder : MonoBinder, ILongBinder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<long, long> _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<long> _set;
        
        /// <summary>
        /// Invokes the event with the specified long value, applying the converter if configured.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(long value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}
