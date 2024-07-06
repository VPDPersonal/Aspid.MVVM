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
        protected sealed override IReadOnlyBindersCollectionById GetBinders() =>
            GetBindersIternal();

        protected virtual BindersCollectionById GetBindersIternal()
        {
            var binders = new BindersCollectionById
            {
                { "Icon", _icon },
                { "Currency", _currency },
            };

            AddBinders(ref binders);
            return binders;
        }

        partial void AddBinders(ref BindersCollectionById binders);
    }
}