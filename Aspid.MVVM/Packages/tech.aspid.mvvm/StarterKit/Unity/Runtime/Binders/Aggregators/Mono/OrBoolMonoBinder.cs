using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="AggregatorMonoBinder{T1, T2}">AggregatorMonoBinder&lt;bool, bool&gt;</see> that forwards
    /// <see langword="true"/> when any input is <see langword="true"/>.
    /// </summary>
    /// <remarks>
    /// The "badge shows if anything needs attention" case: several unrelated flags, one indicator.
    /// </remarks>
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
