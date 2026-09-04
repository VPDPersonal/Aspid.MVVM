using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that turns the bound number into a <see langword="bool"/> through a converter and
    /// invokes one of two <see cref="UnityEvent"/>s.
    /// </summary>
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Number Condition Switcher")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Number Condition Switcher")]
    public sealed partial class UnityEventNumberConditionSwitcherMonoBinder : MonoBinder, IFloatBinder
    {
        [Tooltip("Converter from the number to the condition.")]
        [SerializeReference] private IConverter<float, bool> _converter;

        [Tooltip("Invoked when the condition is true.")]
        [SerializeField] private UnityEvent _trueSet;

        [Tooltip("Invoked when the condition is false.")]
        [SerializeField] private UnityEvent _falseSet;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(float value)
        {
            if (_converter is null)
            {
                this.LogError(
                    problem: "no converter is assigned",
                    consequence: "The value is not forwarded.");

                return;
            }

            if (_converter.Convert(value)) _trueSet?.Invoke();
            else _falseSet?.Invoke();
        }
    }
}
