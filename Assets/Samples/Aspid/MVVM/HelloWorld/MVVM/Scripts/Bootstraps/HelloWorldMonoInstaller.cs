using Zenject;

namespace Aspid.MVVM.Samples.HelloWorld.MVVM
{
    public sealed class HelloWorldMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Speaker>().AsSingle();
            
            Container.Bind<SpeakerViewModel>().AsSingle();
            Container.Bind<MomentSpeakerViewModel>().AsSingle();
        }
    }
}