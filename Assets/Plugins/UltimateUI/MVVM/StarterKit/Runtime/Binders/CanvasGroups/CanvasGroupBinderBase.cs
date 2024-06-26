using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.CanvasGroups
{
    public abstract class CanvasGroupBinderBase : MonoBinder
    {
        [Header("Component")]
        [SerializeField] private CanvasGroup _canvasGroup;
        
        protected CanvasGroup CachedCanvasGroup =>
            _canvasGroup ? _canvasGroup : _canvasGroup = GetComponent<CanvasGroup>();
    }
}