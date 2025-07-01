using Zenject;
using UnityEngine;

namespace Aspid.MVVM.Stats
{
    public sealed class StatsMonoInstaller : MonoInstaller
    {
        [Header("Hero")]
        [SerializeField] [Min(0)] private int _skillPointsAvailable = 7;
        
        public override void InstallBindings()
        {
            Container.Bind<Hero>()
                .AsSingle()
                .WithArguments(_skillPointsAvailable);
            
            Container.Bind<StatsViewModel>().AsTransient();
        }
    }
}