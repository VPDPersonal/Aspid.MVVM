using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that turns the bound number into a <see langword="bool"/> through a converter and
    /// invokes a <see cref="UnityEvent{T}"/> with it.
    /// </summary>
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Number Condition")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Number Condition")]
    public sealed partial class UnityEventNumberConditionMonoBinder : MonoBinder, IFloatBinder
    {
        [Tooltip("Converter from the number to the condition.")]
        [SerializeReference] private IConverter<float, bool> _converter;

        [Tooltip("Invoked with the condition result.")]
        [SerializeField] private UnityEvent<bool> _set;

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

            _set?.Invoke(_converter.Convert(value));
        }
    }
}
