using System;
using UnityEngine;
using System.Collections.Generic;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Collections;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Data;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Models;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample.Economy.ViewModels
{
    [ViewModel]
    public partial class WalletViewModel
    {
        private readonly Wallet _wallet;
        private readonly List<CurrencyViewModel> _currencies;
        
        public WalletViewModel(Wallet wallet, CurrencyViewDataCollection currencyViewDataCollection)
        {
            _wallet = wallet;
            _currencies = new List<CurrencyViewModel>();
            
            foreach (var (type, _) in _wallet)
                _currencies.Add(new CurrencyViewModel(wallet, currencyViewDataCollection, type));
        }

        public void AddNewCurrency()
        {
            Currencies.Add(new CurrencyViewModel(_wallet, null, 0));
        }
    }

    public partial class WalletViewModel : IViewModel
    {
        // TODO IReadOnly
        private event Action<IReadOnlyObservableList<IViewModel>> CurrenciesChanged;
        
        private ObservableList<IViewModel> _currenciesWrapper;
        
        private ObservableList<IViewModel> Currencies
        {
            get
            {
                if (_currencies == null) return null;
                return _currenciesWrapper ??= new ObservableList<IViewModel>(_currencies);
            }
            set
            {
                if (ViewModelUtility.SetProperty(ref _currenciesWrapper, value))
                    CurrenciesChanged?.Invoke(_currenciesWrapper);
            }
        }
        
        public IReadOnlyBindsMethods GetBindMethods()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyBindsMethods GetUnbindMethods()
        {
            throw new NotImplementedException();
        }
    }
}
