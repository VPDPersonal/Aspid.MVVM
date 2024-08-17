using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Views;
using UltimateUI.MVVM.Unity.Generation;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Casters
{
    [AddComponentMenu("UI/Binders/Casters/String Empty To Bool Caster Binder")]
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