using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UltimateUI.MVVM.Views;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Products.Views
{
    public partial class MonoStolenProductView : MonoProductView
    {
        [RequireBinder(typeof(int))]
        [SerializeField] private MonoBinder[] _theftProbability;
        
        [SerializeField] private Button[] _stealButtons;
    }
    
    public partial class MonoStolenProductView
    {
        public override IReadOnlyDictionary<string, IReadOnlyList<IBinder>> GetBinders()
        {
            var binders = new Dictionary<string, IReadOnlyList<IBinder>>
            {
                { "TheftProbability", _theftProbability },
            };
            
            foreach (var pair in base.GetBinders())
                binders.Add(pair.Key, pair.Value);
            
            return binders;
        }
    }
}