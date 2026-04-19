using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Types
{
    public sealed class FastEnemy : EnemyBase
    {
        public override void Attack() =>
            Debug.Log("Fast enemy strikes!");
    }
}
