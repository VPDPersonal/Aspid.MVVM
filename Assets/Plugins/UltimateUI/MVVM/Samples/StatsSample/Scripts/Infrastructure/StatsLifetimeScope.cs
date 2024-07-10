using VContainer;
using UnityEngine;
using VContainer.Unity;
using UltimateUI.MVVM.Samples.StatsSample.Models;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.StatsSample.Infrastructure
{
    public class StatsLifetimeScope : LifetimeScope
    {
        [SerializeField] [Min(0)] private int _skillPointsAvailable = 7;
        
        protected override void Configure(IContainerBuilder builder)
        {
            Application.targetFrameRate = 60;
            builder.Register<Hero>(Lifetime.Singleton).WithParameter(_skillPointsAvailable);
            builder.Register<HeroViewModel>(Lifetime.Singleton);
        }
    }
}
