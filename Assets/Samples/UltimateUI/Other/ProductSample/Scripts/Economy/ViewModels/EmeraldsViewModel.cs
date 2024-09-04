using UltimateUI.MVVM.Samples.ProductSample.Economy.Data;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Models;


namespace UltimateUI.MVVM.Samples.ProductSample.Economy.ViewModels
{
    public class EmeraldsViewModel : CurrencyViewModel
    {
        public EmeraldsViewModel(Wallet wallet, CurrencyViewDataCollection currencyViewDataCollection) 
            : base(wallet, currencyViewDataCollection, CurrencyType.Emeralds) { }
    }
}