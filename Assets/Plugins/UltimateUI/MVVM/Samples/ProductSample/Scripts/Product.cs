using UnityEngine;
using UltimateUI.MVVM.Samples.ProductSample.Economy.Models;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample
{
    [CreateAssetMenu(fileName = "New Product Data", menuName = "MVVM/Samples/Product Sample/Product", order = 0)]
    public class Product : ScriptableObject
    {
        [field: SerializeField]
        public Sprite Icon { get; private set; }
        
        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public CurrencyType CurrencyType { get; private set; }
        
        [field: Min(0)]
        [field: SerializeField]
        public int Price { get; private set; }
        
        [field: TextArea]
        [field: SerializeField]
        public string Description { get; private set; }
    }
}