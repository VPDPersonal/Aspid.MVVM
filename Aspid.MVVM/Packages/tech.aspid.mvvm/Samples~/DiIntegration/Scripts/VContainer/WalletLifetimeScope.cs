#if ASPID_MVVM_VCONTAINER_INTEGRATION
using VContainer;
using VContainer.Unity;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.DiIntegration
{
    // Put this LifetimeScope in the scene. ViewInitializer on the View resolves
    // the ViewModel with Resolve Type = Di and Initialize Stage = DiConstructor.
    public sealed class WalletLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Wallet>(Lifetime.Singleton);
            builder.Register<WalletViewModel>(Lifetime.Singleton);
        }
    }
}
#endif
