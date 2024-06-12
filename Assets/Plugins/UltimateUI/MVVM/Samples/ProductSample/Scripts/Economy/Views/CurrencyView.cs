using UnityEngine;
using UltimateUI.MVVM.Views;
using System.Collections.Generic;

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
        protected IReadOnlyDictionary<string, IReadOnlyList<IBinder>> Binders;
        
        public override IReadOnlyDictionary<string, IReadOnlyList<IBinder>> GetBinders()
        {
            if (Binders != null) return Binders;
            
            return Binders = new Dictionary<string, IReadOnlyList<IBinder>>
            {
                { "Icon", _icon },
                { "Currency", _currency },
            };
        }
    }
}