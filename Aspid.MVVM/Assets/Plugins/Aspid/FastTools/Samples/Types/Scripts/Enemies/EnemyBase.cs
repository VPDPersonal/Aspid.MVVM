using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Types
{
    // Demonstrates ComponentTypeSelector: a serialized marker that adds an Inspector
    // dropdown letting you swap this component's script to any subtype of EnemyBase
    // (FastEnemy, TankEnemy, ...) without removing and re-adding the component.
    //
    // The picker auto-discovers subtypes via the field's declaring class — no
    // configuration needed. Selection rewrites m_Script on the SerializedObject,
    // so all fields persist where the new subtype declares a matching name.
    public abstract class EnemyBase : MonoBehaviour
    {
        // The struct itself is empty; it only carries a PropertyDrawer.
        // Place one field per root class — typically at the top of the Inspector.
        [SerializeField] private ComponentTypeSelector _enemyType;
        
        [SerializeField] [Min(0)] private float _health = 100f;

        protected float Health => _health;

        public abstract void Attack();
    }
}
