using VContainer;
using UnityEngine;
using VContainer.Unity;

namespace Aspid.MVVM.Stats
{
    public sealed class StatsLifetimeScope : LifetimeScope
    {
        [Header("Hero")]
        [SerializeField] [Min(0)] private int _skillPointsAvailable = 7;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Hero>(Lifetime.Singleton)
                .WithParameter(_skillPointsAvailable);
            
            builder.Register<StatsViewModel>(Lifetime.Transient);
        }
    }
}