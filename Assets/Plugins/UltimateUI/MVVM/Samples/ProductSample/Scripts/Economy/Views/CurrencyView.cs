using UnityEngine;
using UltimateUI.MVVM.Views;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.ProductSample.Economy
{
    public partial class CurrencyView : MonoView
    {
        [RequireBinder(typeof(Sprite))]
        [SerializeField] private MonoBinder[] _icon;
        
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _currency;
    }
    
    // View Description
    public partial class CurrencyView
    {
        public override IReadOnlyBindersCollectionById GetBinders()
        {
            return new BindersCollectionById
            {
                { "Icon", _icon },
                { "Currency", _currency },
            };
        }
    }
}