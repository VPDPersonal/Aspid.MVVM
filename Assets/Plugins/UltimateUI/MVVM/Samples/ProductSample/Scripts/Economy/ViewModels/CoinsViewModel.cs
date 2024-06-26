using UltimateUI.MVVM.Samples.ProductSample.Economy.Data;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Models;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample.Economy.ViewModels
{
    public class CoinsViewModel : CurrencyViewModel
    {
        public CoinsViewModel(Wallet wallet, CurrencyViewDataCollection currencyViewDataCollection) 
            : base(wallet, currencyViewDataCollection, CurrencyType.Coins) { }
    }
}