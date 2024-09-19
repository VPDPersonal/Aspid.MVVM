using System;
using UnityEngine;
using AspidUI.MVVM.ViewModels.Generation;
using AspidUI.ProductSample.Economy.Data;
using AspidUI.ProductSample.Economy.Models;

namespace AspidUI.ProductSample.Economy.ViewModels
{
    // [ViewModel]
    public partial class CurrencyViewModel : IDisposable
    {
        [Bind] private Sprite _icon;
        [Bind] private int _currency;
        
        private readonly Wallet _wallet;
        private readonly CurrencyType _currencyType;
        private readonly CurrencyViewDataCollection _currencyViewDataCollection;
        
        public CurrencyViewModel(Wallet wallet, CurrencyViewDataCollection currencyViewDataCollection, CurrencyType currencyType)
        {
            _wallet = wallet;
            _currencyType = currencyType;
            _currencyViewDataCollection = currencyViewDataCollection;
            
            _currency = _wallet[currencyType];
            // UpdateIcon();
        }
        
        public void Initialize()
        {
            // _wallet.AddListener(_currencyType, OnWalletCurrencyChanged);
        }
        
        // private void UpdateIcon() =>
        //     Icon = _currencyViewDataCollection.GetIcon(_currencyType, Currency);
        //
        // private void OnWalletCurrencyChanged(int currency)
        // {
        //     Currency = currency;
        //     UpdateIcon();
        // }
        
        public virtual void Dispose()
        {
            // _wallet.RemoveListener(_currencyType, OnWalletCurrencyChanged);
        }
    }
}