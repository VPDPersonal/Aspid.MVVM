using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Economic.Currency.Data
{
    public sealed class CurrencyViewData : ScriptableObject
    {
        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private Sprite _minIcon;
        [SerializeField] private IconData[] _icons;
        
        public CurrencyType Type => _currencyType;
        
        public Sprite GetIcon(int money)
        {
            if (money <= 0) return _minIcon;
            
            foreach (var data in _icons)
            {
                if (money > data.MinMoney)
                    return data.Icon;
            }
            
            return _minIcon;
        }
        
        [Serializable]
        private struct IconData
        {
            [field: Min(0)]
            [field: SerializeField]
            public int MinMoney { get; private set; }
            
            [field: SerializeField]
            public Sprite Icon { get; private set; }
        }
    }
}