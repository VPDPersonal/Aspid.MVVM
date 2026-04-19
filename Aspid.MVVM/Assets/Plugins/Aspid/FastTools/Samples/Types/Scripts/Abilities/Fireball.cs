using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Types
{
    public sealed class Fireball : Ability
    {
        public override void Activate() =>
            Debug.Log("Fireball launched!");
    }
}
