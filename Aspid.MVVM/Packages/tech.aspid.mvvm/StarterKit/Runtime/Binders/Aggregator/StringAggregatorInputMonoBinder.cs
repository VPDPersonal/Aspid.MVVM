using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AggregatorInputMonoBinder{TInput, TResult}"/> that feeds one <see langword="string"/> into a
    /// <see cref="FormatStringMonoBinder"/>.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/Aggregator/Aggregator Input Binder – String")]
    public sealed class StringAggregatorInputMonoBinder : AggregatorInputMonoBinder<string, string> { }
}
