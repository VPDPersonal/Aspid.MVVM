#if ASPID_MVVM_VCONTAINER_INTEGRATION
using VContainer;
using UnityEngine;
using VContainer.Unity;
using Aspid.MVVM.Stats.Models;
using Aspid.MVVM.Stats.ViewModels;

namespace Aspid.MVVM.Stats
{
    public sealed class StatsLifetimeScope : LifetimeScope
    {
        [Header("Hero")]
        [SerializeField] [Min(0)] private int _skillPointsAvailable = 7;
        
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

        private static void ConfigureViewModel(IContainerBuilder builder)
        {
            builder.Register<EditStatsViewModel>(Lifetime.Singleton);
            builder.Register<ReadOnlyStatsViewModel>(Lifetime.Singleton);
        }
    }
}
#endif
