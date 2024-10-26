#if ASPID_UI_VCONTAINER_INTEGRATION
using VContainer;
using UnityEngine;
using VContainer.Unity;
using Aspid.UI.Stats.Models;
using Aspid.UI.Stats.ViewModels;

namespace Aspid.UI.Stats.VContainer
{
    public sealed class StatsLifetimeScope : LifetimeScope
    {
        [Header("Hero")]
        [SerializeField] [Min(0)] private int _skillPointsAvailable = 7;
        
        [Header("View Models")]
        [SerializeField] private Lifetime _createStatsViewModelLifeTime = Lifetime.Singleton;
        [SerializeField] private Lifetime _readOnlyStatsViewModelLifeTime = Lifetime.Singleton;
        
        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureHero(builder);
            ConfigureViewModel(builder);
        }

        private void ConfigureHero(IContainerBuilder builder)
        {
            builder.Register<Hero>(Lifetime.Singleton)
                .WithParameter(_skillPointsAvailable);
        }

        private void ConfigureViewModel(IContainerBuilder builder)
        {
            builder.Register<EditStatsViewModel>(_createStatsViewModelLifeTime);
            builder.Register<ReadOnlyStatsViewModel>(_readOnlyStatsViewModelLifeTime);
        }
    }
}
#endif
