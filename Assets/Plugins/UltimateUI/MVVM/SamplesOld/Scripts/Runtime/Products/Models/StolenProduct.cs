using UltimateUI.MVVM.Samples.Products.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Products.Models
{
    public class StolenProduct : Product<StolenProductData>
    {
        private int SpeedTheftValue => Data.SpeedTheftValue;
        
        private int MinTheftProbability => Data.MinTheftProbability;
        
        private int MaxTheftProbability => Data.MaxTheftProbability;
        
        public int TheftProbability { get; private set; }
        
        public StolenProduct(StolenProductData data) : base(data) { }
    }
}