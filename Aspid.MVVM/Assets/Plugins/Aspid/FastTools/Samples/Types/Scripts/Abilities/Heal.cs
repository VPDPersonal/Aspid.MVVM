using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Types
{
    public sealed class Heal : Ability
    {
        public override void Activate() =>
            Debug.Log("Heal cast!");
    }
}
