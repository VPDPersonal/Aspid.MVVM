using System;
using UnityEngine;
using System.Collections.Generic;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Data;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Models;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample.Economy.ViewModels
{
    [ViewModel]
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
            UpdateIcon();
        }
        
        public void Initialize()
        {
            _wallet.AddListener(_currencyType, OnCurrencyChanged);
        }
        
        private void UpdateIcon() =>
            Icon = _currencyViewDataCollection.GetIcon(_currencyType, Currency);
        
        private void OnCurrencyChanged(int currency)
        {
            Currency = currency;
            UpdateIcon();
        }
        
        public virtual void Dispose()
        {
            _wallet.RemoveListener(_currencyType, OnCurrencyChanged);
        }
    }
    
    public partial class CurrencyViewModel : IViewModel
    {
        private event Action<Sprite> IconChanged;
        private event Action<int> CurrencyChanged;
        
        private Sprite Icon
        {
            get => _icon;
            set
            {
                if (ViewModelUtility.SetProperty(ref _icon, value)) 
                    IconChanged?.Invoke(_icon);
            }
        }
        
        private int Currency
        {
            get => _currency;
            set
            {
                if (ViewModelUtility.SetProperty(ref _currency, value))
                    CurrencyChanged?.Invoke(_currency);
            }
        }
        
        IReadOnlyBindsMethods IViewModel.GetBindMethods() =>
            GetBindMethods();
        
        protected virtual BindMethods GetBindMethods()
        {
            var bindMethods = new BindMethods
            {
                { nameof(Icon), binders => ViewModelUtility.Bind(_icon, ref IconChanged, binders) },
                { nameof(Currency), binders => ViewModelUtility.Bind(Currency, ref CurrencyChanged, binders) },
            };

            AddBindMethods(ref bindMethods);
            return bindMethods;
        }

        partial void AddBindMethods(ref BindMethods bindMethods);
        
        IReadOnlyBindsMethods IViewModel.GetUnbindMethods() =>
            GetUnbindsMethods();
        
        protected virtual BindMethods GetUnbindsMethods()
        {
            var unbindMethods = new BindMethods
            {
                { nameof(Icon), binders => ViewModelUtility.Unbind(ref IconChanged, binders) },
                { nameof(Currency), binders => ViewModelUtility.Unbind(ref CurrencyChanged, binders) },
            };
            
            AddUnbindMethods(ref unbindMethods);
            return unbindMethods;
        }

        partial void AddUnbindMethods(ref BindMethods bindMethods);
    }
}