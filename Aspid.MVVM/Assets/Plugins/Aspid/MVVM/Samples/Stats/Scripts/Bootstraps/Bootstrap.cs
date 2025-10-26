using UnityEngine;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.Stats
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [Header("Hero")]
        [SerializeField] [Min(0)] private int _skillPointsAvailable = 7;

        [Header("Views")]
        [SerializeField] private EditStatsView _editStatsView;
        [SerializeField] private ReadOnlyStatsView _readOnlyStatsView;
        
        private void Awake()
        {
            var hero = new Hero(_skillPointsAvailable);
            
            _editStatsView.Initialize(new StatsViewModel(hero));
            _readOnlyStatsView.Initialize(new StatsViewModel(hero));
        }

        private void OnDestroy()
        {
            _editStatsView.DeinitializeView()?.DisposeViewModel();
            _readOnlyStatsView.DeinitializeView()?.DisposeViewModel();
        }
    }
}