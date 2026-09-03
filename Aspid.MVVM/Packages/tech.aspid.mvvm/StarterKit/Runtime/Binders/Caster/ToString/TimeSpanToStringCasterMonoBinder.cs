using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ValueToStringCasterMonoBinder{T}"/> for <see cref="TimeSpan"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/TimeSpan To String Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/TimeSpan To String Caster Binder")]
    public sealed class TimeSpanToStringCasterMonoBinder : ValueToStringCasterMonoBinder<TimeSpan> { }
}
