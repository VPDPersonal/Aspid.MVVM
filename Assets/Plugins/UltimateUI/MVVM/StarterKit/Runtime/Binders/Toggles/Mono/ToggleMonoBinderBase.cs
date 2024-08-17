using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Toggles.Mono
{
    [RequireComponent(typeof(Toggle))]
    public abstract class ToggleMonoBinderBase : MonoBinder
    {
        [Header("Component")]
        [SerializeField] private Toggle _toggle;

        private bool _isCached;

        protected virtual void OnValidate()
        {
            if (_toggle)
            {
                _toggle = GetComponent<Toggle>();
            }
            else
            {
                if (GetComponents<Toggle>().Any(toggle => toggle == _toggle)) return;
                if (!_toggle) return;
            
                _toggle = null;
                // TODO Debug Log   
            }
        }

        protected Toggle CachedToggle => _toggle;
    }
}