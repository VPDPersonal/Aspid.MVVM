using UnityEngine;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Models;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample
{
    public class ProductBuyer
    {
        private readonly Wallet _wallet;
        
        public ProductBuyer(Wallet wallet)
        {
            _wallet = wallet;
        }
        
        public bool CanBuy(Product product) =>
            _wallet[product.CurrencyType] >= product.Price;
        
        public void Buy(Product product)
        {
            var purchased = _wallet.TryTake(product.CurrencyType, product.Price) ? "purchased" : "not purchased";
            Debug.Log($"[{nameof(ProductBuyer)}] [{nameof(Buy)}] {product.Name} {purchased}");
        }
    }
}