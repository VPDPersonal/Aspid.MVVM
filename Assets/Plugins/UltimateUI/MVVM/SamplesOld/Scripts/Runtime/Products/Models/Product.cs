using UnityEngine;
using UltimateUI.MVVM.Samples.Products.Data;
using UltimateUI.MVVM.Samples.Economic.Currency;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Products.Models
{
    public abstract class Product<TData> : IProduct
        where TData : ProductData
    {
        protected readonly TData Data;
        
        public Sprite Icon => Data.Icon;
        
        public CurrencyType Currency => Data.Currency;
        
        public int Price => Data.Price;
        
        public string Description => Data.Description;
        
        protected Product(TData data)
        {
            Data = data;
        }
    }
}