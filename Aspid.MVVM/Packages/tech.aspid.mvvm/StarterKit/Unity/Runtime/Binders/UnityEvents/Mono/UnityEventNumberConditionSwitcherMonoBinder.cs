using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that converts the bound numeric ViewModel value to a <see langword="bool"/> using a converter and invokes one of two <see cref="UnityEvent"/> instances based on the result.
    /// </summary>
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Number Condition Switcher")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Number Condition Switcher")]
    public sealed partial class UnityEventNumberConditionSwitcherMonoBinder : MonoBinder, IFloatBinder
    {
        [Tooltip("Required — an empty converter logs an error instead of invoking an event.")]
        [SerializeReference] private IConverter<float, bool> _converter;

        [Tooltip("The event invoked when the condition evaluates to true.")]
        [SerializeField] private UnityEvent _trueSet;
        [Tooltip("The event invoked when the condition evaluates to false.")]
        [SerializeField] private UnityEvent _falseSet;
        
        /// <summary>
        /// Converts the value to a <see langword="bool"/> using the configured converter and invokes the corresponding event.
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

            if (_converter.Convert(value)) _trueSet?.Invoke();
            else _falseSet?.Invoke();
        }
    }
}
