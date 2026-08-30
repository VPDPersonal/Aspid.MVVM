using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that converts the bound numeric ViewModel value to a <see langword="bool"/> using a converter and invokes a <see cref="UnityEvent{T}"/> with the result.
    /// </summary>
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Number Condition")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Number Condition")]
    public sealed partial class UnityEventNumberConditionMonoBinder : MonoBinder, IFloatBinder
    {
        [Tooltip("Required — an empty converter logs an error instead of invoking the event.")]
        [SerializeReference] private IConverter<float, bool> _converter;

        [Tooltip("The event invoked with the boolean result of the condition.")]
        [SerializeField] private UnityEvent<bool> _set;
        
        /// <summary>
        /// Converts the value to a <see langword="bool"/> using the configured converter and invokes the event with the result.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(float value)
        {
            if (_converter is null)
            {
                this.LogError("no converter is assigned", "The value is not forwarded.");
                return;
            }

            _set.Invoke(_converter.Convert(value));
        }
    }
}
