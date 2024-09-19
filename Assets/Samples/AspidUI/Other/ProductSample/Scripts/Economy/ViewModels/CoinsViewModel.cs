using AspidUI.ProductSample.Economy.Data;
using AspidUI.ProductSample.Economy.Models;

namespace AspidUI.ProductSample.Economy.ViewModels
{
    public class CoinsViewModel : CurrencyViewModel
    {
        public CoinsViewModel(Wallet wallet, CurrencyViewDataCollection currencyViewDataCollection) 
            : base(wallet, currencyViewDataCollection, CurrencyType.Coins) { }
    }
}