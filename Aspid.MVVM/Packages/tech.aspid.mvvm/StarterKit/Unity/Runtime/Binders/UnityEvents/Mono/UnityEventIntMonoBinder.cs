using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that invokes a <see cref="UnityEvent{T}"/> with the bound numeric ViewModel value converted to <see cref="int"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(int))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Int")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Int")]
    public sealed partial class UnityEventIntMonoBinder : MonoBinder, INumberBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        [Tooltip("The event invoked with the bound value.")]
        [SerializeField] private UnityEvent<int> _set;

        /// <summary>
        /// Invokes the event with the specified integer value, applying the converter if configured.
        /// </summary>
        [BinderLog]
        public void SetValue(int value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);

        /// <summary>
        /// Converts the value to <see cref="int"/> and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((int)value);

        /// <summary>
        /// Converts the value to <see cref="int"/> and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue((int)value);

        /// <summary>
        /// Converts the value to <see cref="int"/> and invokes the event.
        /// </summary>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((int)value);
    }
}
