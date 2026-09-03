using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that converts a bound
    /// <see cref="string"/> to a <see langword="float"/> with a configurable converter and forwards the result to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// By default, uses <see cref="StringToFloatConverter"/>.
    /// </remarks>
    [AddBinderContextMenuByType(typeof(float))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/String To Float Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/String To Float Caster Binder")]
    public sealed partial class StringToFloatCasterMonoBinder : MonoBinder, IBinder<string>
    {
        [Tooltip("Converter from the bound string to a float.")]
        [SerializeReference] private IConverter<string, float> _converter = new StringToFloatConverter();

        [Tooltip("Invoked with the converted value.")]
        [SerializeField] private UnityEvent<float> _casted;

        private void OnValidate() =>
            _converter ??= new StringToFloatConverter();

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
