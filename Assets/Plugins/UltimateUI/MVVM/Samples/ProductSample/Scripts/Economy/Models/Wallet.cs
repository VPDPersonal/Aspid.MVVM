using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample.Economy.Models
{
    public class Wallet
    {
        private readonly Dictionary<CurrencyType, int> _currency;
        private readonly Dictionary<CurrencyType, Action<int>> _currencyChangedEvents;
        
        public int this[CurrencyType type]
        {
            get => _currency[type];
            private set
            {
                _currency[type] = value;
                _currencyChangedEvents[type]?.Invoke(value);
            }
        }
        
        public Wallet(params (CurrencyType, int)[] currencies)
        {
            var length = currencies.Length;
            _currency = new Dictionary<CurrencyType, int>(length);
            _currencyChangedEvents = new Dictionary<CurrencyType, Action<int>>(length);
            
            foreach (var (type, currency) in currencies)
                _currency.Add(type, currency);
        }
        
        public void AddListener(CurrencyType type, Action<int> currencyChanged)
        {
            if (!_currencyChangedEvents.TryAdd(type, currencyChanged)) 
                _currencyChangedEvents[type] += currencyChanged;
        }
        
        public void RemoveListener(CurrencyType type, Action<int> currencyChanged) =>
            _currencyChangedEvents[type] -= currencyChanged;
        
        public bool TryTake(CurrencyType type, int currency)
        {
            ThrowExceptionIfCoinsLessThanZero(currency);
            if (currency > this[type]) return false;
            
            this[type] -= currency;
            return true;
        }
        
        public void Put(CurrencyType type, int currency)
        {
            ThrowExceptionIfCoinsLessThanZero(currency);
            this[type] += currency;
        }
        
        private static void ThrowExceptionIfCoinsLessThanZero(int coins)
        {
            if (coins < 0) throw new ArgumentException($"Currency can't be less than 0; Currency = {coins}");
        }
    }
}