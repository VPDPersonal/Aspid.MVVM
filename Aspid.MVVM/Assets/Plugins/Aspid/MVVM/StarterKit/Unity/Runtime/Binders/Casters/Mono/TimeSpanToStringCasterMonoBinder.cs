using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="GenericToStringCasterMonoBinder{T}"/> that converts a bound <see cref="TimeSpan"/> to a <see cref="string"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/TimeSpan To String Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/TimeSpan To String Caster Binder")]
#if UNITY_2023_1_OR_NEWER
    public sealed class TimeSpanToStringCasterMonoBinder : GenericToStringCasterMonoBinder<TimeSpan> { }
#else
    public sealed class TimeSpanToStringCasterMonoBinder : GenericToStringCasterMonoBinder<TimeSpan>
    {
        [SerializeReferenceDropdown]
        [Tooltip("The converter used to transform the bound TimeSpan to a string.")]
        [SerializeReference] private IConverterTimeSpanToString _converter = new TimeSpanToStringConverter();

        /// <inheritdoc/>
        protected override IConverter<TimeSpan, string> Converter => _converter;

        /// <summary>
        /// Called by Unity in the Editor when a serialized field value changes.
        /// Assigns the default <see cref="TimeSpanToStringConverter"/> if no converter is set.
        /// </summary>
        private void OnValidate() =>
            _converter ??= new TimeSpanToStringConverter();
    }
#endif
}