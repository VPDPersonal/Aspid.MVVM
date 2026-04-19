using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Types
{
    public sealed class DoubleDamageModifier : AbilityModifier
    {
        public override void Apply() =>
            Debug.Log($"{nameof(DoubleDamageModifier)} applied.");
    }
}
