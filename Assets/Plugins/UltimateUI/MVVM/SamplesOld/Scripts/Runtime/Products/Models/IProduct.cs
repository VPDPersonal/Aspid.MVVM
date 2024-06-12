using UnityEngine;
using UltimateUI.MVVM.Samples.Economic.Currency;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Products.Models
{
    public interface IProduct
    {
        public Sprite Icon { get; }
        
        public CurrencyType Currency { get; }
        
        public int Price { get; }
        
        public string Description { get; }
    }
}