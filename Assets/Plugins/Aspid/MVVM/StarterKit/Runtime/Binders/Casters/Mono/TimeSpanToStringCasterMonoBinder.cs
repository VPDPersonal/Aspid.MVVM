using System;
using UnityEngine;
#if !UNITY_2023_1_OR_NEWER
using Aspid.MVVM.StarterKit.Converters;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Casters/TimeSpan To String Caster Binder")]
#if UNITY_2023_1_OR_NEWER
    public sealed class TimeSpanToStringCasterMonoBinder : GenericToStringCasterMonoBinder<TimeSpan> { }
#else
    public sealed class TimeSpanToStringCasterMonoBinder : GenericToStringCasterMonoBinder<TimeSpan>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private IConverterTimeSpanToString _converter = new TimeSpanToStringConverter();

        protected override IConverter<TimeSpan, string> Converter => _converter;
    }
#endif
}