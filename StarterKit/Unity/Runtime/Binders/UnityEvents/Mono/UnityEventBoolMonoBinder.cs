using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Bool")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Bool")]
    public sealed partial class UnityEventBoolMonoBinder : MonoBinder, IBinder<bool>
    {
        [Tooltip("When enabled, the boolean value is logically inverted before being passed to the event.")]
        [SerializeField] private bool _isInvert;
        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<bool> _set;

        /// <summary>
        /// Invokes the event with the specified boolean value, applying inversion if configured.
        /// </summary>
        [BinderLog]
        public void SetValue(bool value) =>
            _set?.Invoke(_isInvert ? !value : value);
    }
}
