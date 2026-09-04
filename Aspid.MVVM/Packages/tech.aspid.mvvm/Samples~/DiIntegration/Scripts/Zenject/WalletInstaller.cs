#if ASPID_MVVM_ZENJECT_INTEGRATION
using Zenject;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.DiIntegration
{
    // Add this installer to the SceneContext. ViewInitializer on the View resolves
    // the ViewModel with Resolve Type = Di and Initialize Stage = DiConstructor.
    public sealed class WalletInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Wallet>().AsSingle();
            Container.Bind<WalletViewModel>().AsSingle();
        }
    }
}
#endif
