using Aspid.UI.ProductSample.Economy.Data;
using Aspid.UI.ProductSample.Economy.Models;

namespace Aspid.UI.ProductSample.Economy.ViewModels
{
    public class CoinsViewModel : CurrencyViewModel
    {
        public CoinsViewModel(Wallet wallet, CurrencyViewDataCollection currencyViewDataCollection) 
            : base(wallet, currencyViewDataCollection, CurrencyType.Coins) { }
    }
}