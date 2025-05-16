using System;
using Aspid.MVVM.Currency.Models;

namespace Aspid.MVVM.Currency.ViewModel
{
    [ViewModel]
    public partial class CurrencyViewModel_2 : IDisposable
    {
        [OneWayBind] private int _soft;
        [OneWayBind] private int _hard;

        private readonly IShop _shop;
        private readonly Wallet _wallet;

        public CurrencyViewModel_2(IShop shop, Wallet wallet)
        {
            _shop = shop;
            _wallet = wallet;

            _soft = _wallet.Soft;
            _hard = _wallet.Hard;
            
            _wallet.SoftChanged += SetSoft;
            _wallet.HardChanged += SetHard;
        }

        [RelayCommand]
        private void OpenShop(ShopCategory category) => 
            _shop.Open(category);

        public void Dispose()
        {
            _wallet.SoftChanged -= SetSoft;
            _wallet.HardChanged -= SetHard;
        }
    }
}