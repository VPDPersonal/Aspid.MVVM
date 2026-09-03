using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that converts a bound
    /// <see cref="string"/> to an <see langword="int"/> with a configurable converter and forwards the result to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// By default, uses <see cref="StringToIntConverter"/>.
    /// </remarks>
    [AddBinderContextMenuByType(typeof(int))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/String To Int Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/String To Int Caster Binder")]
    public sealed partial class StringToIntCasterMonoBinder : MonoBinder, IBinder<string>
    {
        [Tooltip("Converter from the bound string to an int.")]
        [SerializeReference] private IConverter<string, int> _converter = new StringToIntConverter();

        [Tooltip("Invoked with the converted value.")]
        [SerializeField] private UnityEvent<int> _casted;

        private void OnValidate() =>
            _converter ??= new StringToIntConverter();

        /// <summary>
        /// Converts <paramref name="value"/> with the configured converter and invokes the target <see cref="UnityEvent{T}"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        /// <remarks>
        /// With no converter assigned, logs an error and forwards nothing.
        /// </remarks>
        [BinderLog]
        public void SetValue(string value)
        {
            if (_converter is null)
            {
                this.LogError("no converter is assigned", "The value is not forwarded.");
                return;
            }

            _casted?.Invoke(_converter.Convert(value));
        }
    }
}
