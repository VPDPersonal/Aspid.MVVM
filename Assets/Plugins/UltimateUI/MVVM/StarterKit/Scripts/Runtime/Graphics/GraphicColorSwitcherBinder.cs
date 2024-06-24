using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Graphics
{
    public class GraphicColorSwitcherBinder : GraphicBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Color _trueColor;
        [SerializeField] private Color _falseColor;

        protected Color TrueColor => _trueColor;
        
        protected Color FalseColor => _falseColor;
        
        public void SetValue(bool value) =>
            CachedGraphic.color = value ? TrueColor : FalseColor;
    }
}