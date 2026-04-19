using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Types
{
    public sealed class CooldownReductionModifier : AbilityModifier
    {
        public override void Apply() =>
            Debug.Log($"{nameof(CooldownReductionModifier)} applied.");
    }
}
