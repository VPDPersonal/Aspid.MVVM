using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Views;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Products.Views
{
    public partial class MonoProductView : MonoView
    {
        [SerializeField] private Button[] _buyButtons;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _price;
        
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _description;
        
        [RequireBinder(typeof(Sprite))]
        [SerializeField] private MonoBinder[] _productIcon;
        
        [RequireBinder(typeof(Sprite))] 
        [SerializeField] private MonoBinder[] _currencyIcon;
    }
    
    public partial class MonoProductView
    {
        public override IReadOnlyDictionary<string, IReadOnlyList<IBinder>> GetBinders() => new Dictionary<string, IReadOnlyList<IBinder>>
        {
            { "Price", _price },
            { "Description", _description },
            { "ProductIcon", _productIcon },
            { "CurrencyIcon", _currencyIcon }
        };
    }
}