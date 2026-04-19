using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Ids
{
    [CreateAssetMenu(fileName = "New Enemy Definition", menuName = "Aspid/FastTools/Samples/Enemy Definition")]
    public class EnemyDefinition : ScriptableObject
    {
        [UniqueId]
        [SerializeField] private EnemyId _id;

        [SerializeField] private string _displayName;
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _moveSpeed;

        public EnemyId Id => _id;
        public string DisplayName => _displayName;
        public int MaxHealth => _maxHealth;
        public float MoveSpeed => _moveSpeed;
    }
}
