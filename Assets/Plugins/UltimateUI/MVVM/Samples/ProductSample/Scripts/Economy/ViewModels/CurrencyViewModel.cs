using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Data;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Models;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample.Economy.ViewModels
{
    
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
                if (ViewModelUtility.SetField(ref _icon, value)) 
                    IconChanged?.Invoke(_icon);
            }
        }
        
        private int Currency
        {
            get => _currency;
            set
            {
                if (ViewModelUtility.SetField(ref _currency, value))
                    CurrencyChanged?.Invoke(_currency);
            }
        }
        
        IReadOnlyDictionary<string, Action<IReadOnlyCollection<IBinder>>> IViewModel.GetBindMethods() =>
            GetBindMethods();
        
        protected virtual Dictionary<string, Action<IReadOnlyCollection<IBinder>>> GetBindMethods()
        {
            return new Dictionary<string, Action<IReadOnlyCollection<IBinder>>>
            {
                { nameof(Icon), binders => ViewModelUtility.Bind(_icon, ref IconChanged, binders) },
                { nameof(Currency), binders => ViewModelUtility.Bind(Currency, ref CurrencyChanged, binders) },
            };
        }
        
        IReadOnlyDictionary<string, Action<IReadOnlyCollection<IBinder>>> IViewModel.GetUnbindsMethods() =>
            GetUnbindsMethods();
        
        protected virtual Dictionary<string, Action<IReadOnlyCollection<IBinder>>> GetUnbindsMethods()
        {
            return new Dictionary<string, Action<IReadOnlyCollection<IBinder>>>
            {
                { nameof(Icon), binders => ViewModelUtility.Unbind(ref IconChanged, binders) },
                { nameof(Currency), binders => ViewModelUtility.Unbind(ref CurrencyChanged, binders) },
            };
        }
    }
}