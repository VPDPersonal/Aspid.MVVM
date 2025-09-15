using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/TimeSpan To String Caster Binder")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/Casters/TimeSpan To String Caster Binder")]
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