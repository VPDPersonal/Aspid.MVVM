using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="AggregatorMonoBinder{T1, T2}">AggregatorMonoBinder&lt;bool, bool&gt;</see> that forwards
    /// <see langword="true"/> only when every input is <see langword="true"/>.
    /// </summary>
    /// <remarks>
    /// The "button is available when three things hold" case, which used to need a fourth field in the ViewModel to hold
    /// the answer.
    /// </remarks>
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
