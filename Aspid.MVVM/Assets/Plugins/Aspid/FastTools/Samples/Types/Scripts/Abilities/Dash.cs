using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Types
{
    public sealed class Dash : Ability
    {
        public override void Activate() =>
            Debug.Log("Dash performed!");
    }
}
