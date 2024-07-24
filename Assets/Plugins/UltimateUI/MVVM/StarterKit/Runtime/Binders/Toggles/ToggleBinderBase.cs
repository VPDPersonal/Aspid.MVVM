using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Toggles
{
    public abstract class ToggleBinderBase : MonoBinder
    {
        [Header("Component")]
        [SerializeField] private Toggle _toggle;
        
        protected Toggle CachedToggle => _toggle ? _toggle : _toggle = GetComponent<Toggle>();
    }
}