using UnityEngine;
using TriInspector;
using UltimateUI.MVVM.Samples.Economic.Currency;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Products.Data
{
    [DeclareHorizontalGroup("Currency")]
    [CreateAssetMenu(fileName = "New Product Data", menuName = "UltimateUI/Samples/Product Data", order = 0)]
    public class ProductData : ScriptableObject
    {
        [field: SerializeField]
        public Sprite Icon { get; private set; }
        
        [field: SerializeField]
        [field: Group("Currency")]
        public CurrencyType Currency { get; private set; }
        
        [field: Min(0)]
        [field: LabelText("")]
        [field: SerializeField]
        [field: Group("Currency")]
        public int Price { get; private set; }
        
        [field: Multiline]
        [field: SerializeField]
        public string Description { get; private set; }
    }
}