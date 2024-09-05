#if UNITY_2023_1_OR_NEWER
using System;
using UnityEngine;

namespace UltimateUI.MVVM.StarterKit.Binders.Mono.Casters
{
    [AddComponentMenu("UI/Binders/Casters/TimeSpan To String Caster Binder")]
    public sealed class TimeSpanToStringCasterMonoBinder : GenericToStringCasterMonoBinder<TimeSpan> { }
}
#endif