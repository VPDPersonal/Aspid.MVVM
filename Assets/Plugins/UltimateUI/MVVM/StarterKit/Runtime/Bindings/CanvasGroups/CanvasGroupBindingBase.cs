using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.CanvasGroups
{
    public abstract class CanvasGroupBindingBase : MonoBinding
    {
        [Header("Component")]
        [SerializeField] private CanvasGroup _canvasGroup;
        
        protected CanvasGroup CachedCanvasGroup =>
            _canvasGroup ? _canvasGroup : _canvasGroup = GetComponent<CanvasGroup>();
    }
}