using UltimateUI.MVVM.Samples.Products.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Products.Models
{
    public sealed class SimpleProduct : Product<ProductData>
    {
        public SimpleProduct(ProductData data) : base(data) { }
    }
}