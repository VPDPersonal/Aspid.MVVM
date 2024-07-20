using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.CanvasGroups
{
    [AddComponentMenu("UI/Binders/Canvas Group/Canvas Group Binder - Alpha Switcher")]
    public partial class CanvasGroupAlphaSwitcherBinder : CanvasGroupBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] [Range(0, 1)] private float _trueAlpha;
        [SerializeField] [Range(0, 1)] private float _falseAlpha;

        protected float TrueAlpha => _trueAlpha;
        
        protected float FalseAlpha => _falseAlpha;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedCanvasGroup.alpha = GetAlpha(value);
        
        protected float GetAlpha(bool value) =>
            value ? TrueAlpha : FalseAlpha;
    }
}