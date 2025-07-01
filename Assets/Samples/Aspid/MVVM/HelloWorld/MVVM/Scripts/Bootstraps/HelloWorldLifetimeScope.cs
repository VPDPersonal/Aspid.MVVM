using VContainer;
using VContainer.Unity;

namespace Aspid.MVVM.Samples.HelloWorld.MVVM
{
    public sealed class HelloWorldLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Speaker>(Lifetime.Singleton);
            
            builder.Register<SpeakerViewModel>(Lifetime.Singleton);
            builder.Register<MomentSpeakerViewModel>(Lifetime.Singleton);
        }
    }
}