using Aspid.Collections.Observable;
using Aspid.UI.MVVM.ViewModels.Generation;
using Aspid.UI.ProductSample.Economy.Data;
using Aspid.UI.ProductSample.Economy.Models;

namespace Aspid.UI.ProductSample.Economy.ViewModels
{
    // [ViewModel]
    public partial class WalletViewModel
    {
        private readonly Wallet _wallet;
        [Bind] private ObservableList<CurrencyViewModel> _currencies;
        
        public WalletViewModel(Wallet wallet, CurrencyViewDataCollection currencyViewDataCollection)
        {
            _wallet = wallet;
            _currencies = new ObservableList<CurrencyViewModel>();
            
            foreach (var (type, _) in _wallet)
                _currencies.Add(new CurrencyViewModel(wallet, currencyViewDataCollection, type));
        }

        public void AddNewCurrency()
        {
            // Currencies.Add(new CurrencyViewModel(_wallet, null, 0));
        }
    }
}
