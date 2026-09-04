using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AggregatorMonoBinder{TInput, TResult}"/> that forwards <see langword="true"/> only when every input
    /// is <see langword="true"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Aggregator/Aggregator – And")]
    public sealed class AndBoolMonoBinder : AggregatorMonoBinder<bool, bool>
    {
        /// <inheritdoc/>
        protected override bool Combine(bool[] values)
        {
            foreach (var value in values)
            {
                if (!value) return false;
            }

            return true;
        }
    }
}
