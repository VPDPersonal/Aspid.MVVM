using AspidUI.ProductSample.Economy.Data;
using AspidUI.ProductSample.Economy.Models;

namespace AspidUI.ProductSample.Economy.ViewModels
{
    public class EmeraldsViewModel : CurrencyViewModel
    {
        public EmeraldsViewModel(Wallet wallet, CurrencyViewDataCollection currencyViewDataCollection) 
            : base(wallet, currencyViewDataCollection, CurrencyType.Emeralds) { }
    }
}