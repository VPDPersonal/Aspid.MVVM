using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AggregatorMonoBinder{TInput, TResult}"/> that forwards <see langword="true"/> when any input is
    /// <see langword="true"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Aggregator/Aggregator – Or")]
    public sealed class OrBoolMonoBinder : AggregatorMonoBinder<bool, bool>
    {
        /// <inheritdoc/>
        protected override bool Combine(bool[] values)
        {
            foreach (var value in values)
            {
                if (value) return true;
            }

            return false;
        }
    }
}
