using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Products.Data
{
    [CreateAssetMenu(fileName = "New Stolen Product Data", menuName = "UltimateUI/Samples/Stolen Product Data", order = 0)]
    public class StolenProductData : ProductData
    {
        [field: Min(0)]
        [field: SerializeField]
        public int SpeedTheftValue { get; private set; }
        
        [field: Min(0)]
        [field: SerializeField]
        public int MinTheftProbability { get; private set; }
        
        [field: Min(0)]
        [field: SerializeField]
        public int MaxTheftProbability { get; private set; }
        
        private void OnValidate()
        {
            if (MinTheftProbability > MaxTheftProbability)
                MaxTheftProbability = MinTheftProbability;
        }
    }
}