using System;
using UnityEngine;
using System.Collections.Generic;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Data;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Models;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample.Economy.ViewModels
{
    public partial class CurrencyViewModel : IViewModel, IDisposable
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
            _icon = _currencyViewDataCollection.GetIcon(_currencyType, _currency);
        
        private void OnCurrencyChanged(int currency)
        {
            _currency = currency;
            UpdateIcon();
        }
        
        public virtual void Dispose()
        {
            _wallet.RemoveListener(_currencyType, OnCurrencyChanged);
        }
    }
    
    public partial class CurrencyViewModel
    {
        private event Action<Sprite> IconChanged;
        private event Action<int> CurrencyChanged;
        
        private Sprite Icon
        {
            get => _icon;
            set => ViewModelUtility.SetValue(ref _icon, value, IconChanged);
        }
        
        private int Currency
        {
            get => _currency;
            set => ViewModelUtility.SetValue(ref _currency, value, CurrencyChanged);
        }
        
        public virtual IReadOnlyDictionary<string, Action<IReadOnlyCollection<IBinder>>> GetBinds()
        {
            return new Dictionary<string, Action<IReadOnlyCollection<IBinder>>>
            {
                { nameof(Icon), binders => ViewModelUtility.Bind(_icon, ref IconChanged, binders) },
                { nameof(Currency), binders => ViewModelUtility.Bind(Currency, ref CurrencyChanged, binders) },
            };
        }
        
        public virtual IReadOnlyDictionary<string, Action<IReadOnlyCollection<IBinder>>> GetUnbinds()
        {
            return new Dictionary<string, Action<IReadOnlyCollection<IBinder>>>
            {
                { nameof(Icon), binders => ViewModelUtility.Unbind(ref IconChanged, binders) },
                { nameof(Currency), binders => ViewModelUtility.Unbind(ref CurrencyChanged, binders) },
            };
        }
    }
}