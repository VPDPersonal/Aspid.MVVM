using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AggregatorInputMonoBinder{TInput, TResult}"/> that feeds one <see langword="bool"/> into an
    /// <see cref="AndBoolMonoBinder"/> or <see cref="OrBoolMonoBinder"/>.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(bool))]
    [AddComponentMenu("Aspid/MVVM/Binders/Aggregator/Aggregator Input Binder – Bool")]
    public sealed class BoolAggregatorInputMonoBinder : AggregatorInputMonoBinder<bool, bool> { }
}
