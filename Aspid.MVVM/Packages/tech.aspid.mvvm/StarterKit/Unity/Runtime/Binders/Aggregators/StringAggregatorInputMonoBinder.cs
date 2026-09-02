using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="AggregatorInputMonoBinder{T1, T2}">AggregatorInputMonoBinder&lt;string, string&gt;</see> that
    /// feeds one string into a <see cref="FormatStringMonoBinder"/>.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/Aggregator/Aggregator Input Binder – String")]
    public sealed class StringAggregatorInputMonoBinder : AggregatorInputMonoBinder<string, string> { }
}
