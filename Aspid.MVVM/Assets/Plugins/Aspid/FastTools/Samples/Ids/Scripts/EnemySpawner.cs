using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.Ids
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDefinition[] _catalog;
        [SerializeField] private EnemyId _spawnTarget;

        private void Start()
        {
            foreach (var enemy in _catalog)
            {
                if (enemy.Id.Id != _spawnTarget.Id) continue;

                Debug.Log($"Spawning {enemy.DisplayName} — HP: {enemy.MaxHealth}, Speed: {enemy.MoveSpeed}");
                return;
            }

            Debug.LogWarning($"No EnemyDefinition found for id {_spawnTarget.Id}");
        }
    }
}
