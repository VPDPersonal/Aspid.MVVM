using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Economic.Currency.Data
{
    public sealed class CurrencyViewDataCollection : ScriptableObject
    {
        [SerializeField] private CurrencyViewData[] _currencyData;
        
        public Sprite GetIcon(CurrencyType type, int money) =>
            _currencyData.First(data => data.Type == type).GetIcon(money);
    }
}