using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.DiIntegration
{
    [View]
    public sealed partial class WalletView : MonoView
    {
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _coins;

        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private MonoBinder[] _earnCommand;
    }
}
