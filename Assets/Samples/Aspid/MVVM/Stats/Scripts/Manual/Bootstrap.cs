using UnityEngine;
using Aspid.MVVM.Stats.Views;
using Aspid.MVVM.Stats.Models;
using Aspid.MVVM.Stats.ViewModels;
using Aspid.MVVM.Views.Extensions;
using Aspid.MVVM.ViewModels.Extensions;

namespace Aspid.MVVM.Stats.Manual
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
            _editStatsView.Initialize(new EditStatsViewModel(hero));
            _readOnlyStatsView.Initialize(new ReadOnlyStatsViewModel(hero));
        }

        private void OnDestroy()
        {
            _editStatsView.DeinitializeView()?.DisposeViewModel();
            _readOnlyStatsView.DeinitializeView()?.DisposeViewModel();
        }
    }
}