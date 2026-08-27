using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="AggregatorInputMonoBinder{T1, T2}">AggregatorInputMonoBinder&lt;bool, bool&gt;</see> that feeds
    /// one boolean into an <see cref="AndBoolMonoBinder"/> or an <see cref="OrBoolMonoBinder"/>.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/Aggregator/Aggregator Input Binder – Bool")]
    [AddBinderContextMenuByType(typeof(bool))]
    public sealed partial class BoolAggregatorInputMonoBinder : AggregatorInputMonoBinder<bool, bool> { }
}
