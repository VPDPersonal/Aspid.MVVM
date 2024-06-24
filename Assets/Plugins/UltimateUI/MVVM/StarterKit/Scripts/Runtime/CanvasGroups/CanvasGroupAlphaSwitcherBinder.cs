using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.CanvasGroups
{
    public class CanvasGroupAlphaSwitcherBinder : CanvasGroupBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] [Range(0, 1)] private float _trueAlpha;
        [SerializeField] [Range(0, 1)] private float _falseAlpha;

        protected float TrueAlpha => _trueAlpha;
        
        protected float FalseAlpha => _falseAlpha;
        
        public void SetValue(bool value) =>
            CachedCanvasGroup.alpha = value ? TrueAlpha : FalseAlpha;
    }
}