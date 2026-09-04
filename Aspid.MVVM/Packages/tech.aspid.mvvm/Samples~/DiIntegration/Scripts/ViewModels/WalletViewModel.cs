using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.DiIntegration
{
    // The container creates this ViewModel and passes the model through the constructor.
    [ViewModel]
    public sealed partial class WalletViewModel : IDisposable
    {
        [OneWayBind] private int _coins;

        private readonly Wallet _wallet;

        public WalletViewModel(Wallet wallet)
        {
            _wallet = wallet;
            _coins = wallet.Coins;

            _wallet.CoinsChanged += SetCoins;
        }

        [RelayCommand]
        private void Earn() =>
            _wallet.Add(10);

        public void Dispose() =>
            _wallet.CoinsChanged -= SetCoins;
    }
}
