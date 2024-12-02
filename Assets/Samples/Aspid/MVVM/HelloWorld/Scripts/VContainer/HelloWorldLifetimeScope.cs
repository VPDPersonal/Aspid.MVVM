#if ASPID_MVVM_VCONTAINER_INTEGRATION
using VContainer;
using VContainer.Unity;
using Aspid.MVVM.HelloWorld.Models;
using Aspid.MVVM.HelloWorld.ViewModels;

namespace Aspid.MVVM.HelloWorld.VContainer
{
    public sealed class HelloWorldLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Speaker>(Lifetime.Singleton);
            
            builder.Register<MomentSpeakerViewModel>(Lifetime.Singleton);
            builder.Register<CommandSpeakerViewModel1>(Lifetime.Singleton);
            builder.Register<CommandSpeakerViewModel2>(Lifetime.Singleton);
        }
    }
}
#endif