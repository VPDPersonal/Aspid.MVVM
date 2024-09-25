using Aspid.UI.ProductSample.Economy.Data;
using Aspid.UI.ProductSample.Economy.Models;

namespace Aspid.UI.ProductSample.Economy.ViewModels
{
    public class EmeraldsViewModel : CurrencyViewModel
    {
        public EmeraldsViewModel(Wallet wallet, CurrencyViewDataCollection currencyViewDataCollection) 
            : base(wallet, currencyViewDataCollection, CurrencyType.Emeralds) { }
    }
}