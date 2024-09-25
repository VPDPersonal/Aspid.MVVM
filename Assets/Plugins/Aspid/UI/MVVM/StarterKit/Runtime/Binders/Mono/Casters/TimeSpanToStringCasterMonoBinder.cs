using System;
using UnityEngine;
#if !UNITY_2023_1_OR_NEWER
using Aspid.UI.MVVM.StarterKit.Converters;
using Aspid.UI.MVVM.StarterKit.Converters.Strings;
#endif

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Casters
{
    [AddComponentMenu("UI/Binders/Casters/TimeSpan To String Caster Binder")]
#if UNITY_2023_1_OR_NEWER
    public sealed class TimeSpanToStringCasterMonoBinder : GenericToStringCasterMonoBinder<TimeSpan> { }
#else
    public sealed class TimeSpanToStringCasterMonoBinder : GenericToStringCasterMonoBinder<TimeSpan>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterTimeSpanToString _converter = new TimeSpanToStringConverter();

        protected override IConverter<TimeSpan, string> Converter => _converter;
    }
#endif
}