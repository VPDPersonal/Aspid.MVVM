using System;
using Aspid.MVVM.Currency.Models;

namespace Aspid.MVVM.Currency.ViewModel
{
    [ViewModel]
    public partial class CurrencyViewModel_1 : IDisposable
    {
        [OneWayBind] private int _soft;
        [OneWayBind] private int _hard;
        
        private readonly Wallet _wallet;

        public CurrencyViewModel_1(Wallet wallet)
        {
            _wallet = wallet;

            _soft = _wallet.Soft;
            _hard = _wallet.Hard;
            
            _wallet.SoftChanged += SetSoft;
            _wallet.HardChanged += SetHard;
        }

        public void Dispose()
        {
            _wallet.SoftChanged -= SetSoft;
            _wallet.HardChanged -= SetHard;
        }
    }
}