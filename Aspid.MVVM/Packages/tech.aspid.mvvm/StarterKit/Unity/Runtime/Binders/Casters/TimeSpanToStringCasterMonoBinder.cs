using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ToStringCasterMonoBinder{T}"/> that converts a bound <see cref="TimeSpan"/> to a <see cref="string"/>.
    /// </summary>
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/Casters/TimeSpan To String Caster Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Casters/TimeSpan To String Caster Binder")]
    public sealed class TimeSpanToStringCasterMonoBinder : ToStringCasterMonoBinder<TimeSpan> { }
}