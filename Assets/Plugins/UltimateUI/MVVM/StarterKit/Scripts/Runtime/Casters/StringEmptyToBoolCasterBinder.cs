using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Casters
{
    public class StringEmptyToBoolCasterBinder : IBinder<string>
    {
        // TODO Validate
        [Header("Binders")]
        [SerializeField] private MonoBinder[] _binders;
        
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