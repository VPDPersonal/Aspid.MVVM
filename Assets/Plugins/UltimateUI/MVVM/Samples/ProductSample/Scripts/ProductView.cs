using UnityEngine;
using UltimateUI.MVVM.Views;
using System.Collections.Generic;

namespace UltimateUI.MVVM.Samples.ProductSample
{
    public partial class ProductView : MonoView
    {
        [RequireBinder(typeof(Sprite))]
        [SerializeField] private MonoBinder[] _icon;
        
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _name;
        
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _description;
        
        [RequireBinder(typeof(Sprite))]
        [SerializeField] private Sprite _currencyIcon;
    }
    
    public partial class ProductView
    {
        protected Dictionary<string, IReadOnlyList<IBinder>> Binders;
        
        public override IReadOnlyDictionary<string, IReadOnlyList<IBinder>> GetBinders()
        {
            if (Binders != null) return Binders;
            
            return Binders = new Dictionary<string, IReadOnlyList<IBinder>>()
            {
                { "Icon", _icon },
                { "Name", _name },
                { "Icon", _description },
            };
        }
    }
}