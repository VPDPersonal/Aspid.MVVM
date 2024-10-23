using VContainer;
using VContainer.Unity;
using Aspid.UI.HelloWorld.Models;
using Aspid.UI.HelloWorld.ViewModels;

namespace Aspid.UI.HelloWorld.VContainer
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