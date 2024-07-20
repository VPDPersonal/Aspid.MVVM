using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.CanvasGroups
{
    [AddComponentMenu("UI/Binders/Canvas Group/Canvas Group Binder - Alpha")]
    public partial class CanvasGroupAlphaBinder : CanvasGroupBinderBase, IBinder<bool>, IBinder<float>
    {
        [BinderLog]
        public void SetValue(bool value) =>
            CachedCanvasGroup.alpha = value ? 1 : 0;
        
        [BinderLog]
        public void SetValue(float value) =>
            CachedCanvasGroup.alpha = Mathf.Clamp(value, 0, 1);
    }
}