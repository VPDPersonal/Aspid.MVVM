using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.CanvasGroups
{
    public class CanvasGroupAlphaBinder : CanvasGroupBinderBase, IBinder<bool>, IBinder<float>
    {
        public void SetValue(bool value) =>
            CachedCanvasGroup.alpha = value ? 1 : 0;
        
        public void SetValue(float value) =>
            CachedCanvasGroup.alpha = Mathf.Clamp(value, 0, 1);
    }
}