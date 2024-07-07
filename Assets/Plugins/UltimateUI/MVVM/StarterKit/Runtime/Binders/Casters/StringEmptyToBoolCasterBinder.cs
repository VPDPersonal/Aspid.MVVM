using UnityEngine;
using UltimateUI.MVVM.Views;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Casters
{
    public partial class StringEmptyToBoolCasterBinder : MonoBinder, IBinder<string>
    {
        [Header("Binders")]
        [RequireBinder(typeof(bool))]
        [SerializeField] private MonoBinder[] _binders;
        
        [BinderLog]
        public void SetValue(string value)
        {
            var casterValue = string.IsNullOrEmpty(value);
            
            foreach (var binder in _binders)
            {
                if (binder is IBinder<bool> boolBinder)
                    boolBinder.SetValue(casterValue);
            }
        }
    }
}