using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Types
{
    public sealed class RangeBoostModifier : AbilityModifier
    {
        public override void Apply() =>
            Debug.Log($"{nameof(RangeBoostModifier)} applied.");
    }
}
