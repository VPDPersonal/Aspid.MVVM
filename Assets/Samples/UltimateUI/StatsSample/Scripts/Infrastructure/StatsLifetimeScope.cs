using VContainer;
using UnityEngine;
using VContainer.Unity;
using UltimateUI.Samples.StatsSample.Models;
using UltimateUI.Samples.StatsSample.ViewModels;


namespace UltimateUI.Samples.StatsSample.Infrastructure
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
            Application.targetFrameRate = 60;

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
            builder.Register<CreateStatsViewModel>(_createStatsViewModelLifeTime);
            builder.Register<ReadOnlyStatsViewModel>(_readOnlyStatsViewModelLifeTime);
        }
    }
}
