using System.Linq;
using UnityEngine;
using AspidUI.ProductSample.Economy.Models;

namespace AspidUI.ProductSample.Economy.Data
{
    [CreateAssetMenu(fileName = "New Currency View Data Collection", menuName = "MVVM/Samples/Product Sample/Economy/Currency View Collection", order = 0)]
    public sealed class CurrencyViewDataCollection : ScriptableObject
    {
        [SerializeField] private CurrencyViewData[] _currencyData;
        
        public Sprite GetIcon(CurrencyType type, int money) =>
            _currencyData.First(data => data.Type == type).GetIcon(money);
    }
}