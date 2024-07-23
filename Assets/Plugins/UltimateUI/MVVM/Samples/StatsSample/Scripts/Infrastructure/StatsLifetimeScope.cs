using VContainer;
using UnityEngine;
using VContainer.Unity;
using UltimateUI.MVVM.Samples.StatsSample.Models;
using UltimateUI.MVVM.Samples.StatsSample.ModelViews;
using UltimateUI.MVVM.Samples.StatsSample.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.StatsSample.Infrastructure
{
    public sealed class StatsLifetimeScope : LifetimeScope
    {
        [SerializeField] [Min(0)] private int _skillPointsAvailable = 7;
        [SerializeField] private Lifetime _createStatsViewModelLifeTime = Lifetime.Singleton;
        
        protected override void Configure(IContainerBuilder builder)
        {
            Application.targetFrameRate = 60;

            builder.Register<ReadOnlyStatsViewModel>(Lifetime.Singleton);
            builder.Register<CreateStatsViewModel>(_createStatsViewModelLifeTime);
            builder.Register<Hero>(Lifetime.Singleton).WithParameter(_skillPointsAvailable);
        }
    }
}
