using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Types
{
    public sealed class TankEnemy : EnemyBase
    {
        public override void Attack() =>
            Debug.Log("Tank attacks!");
    }
}
