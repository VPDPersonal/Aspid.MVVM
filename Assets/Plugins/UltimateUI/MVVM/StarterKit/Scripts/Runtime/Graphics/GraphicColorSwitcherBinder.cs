using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Graphics
{
    public class GraphicColorSwitcherBinder : GraphicBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Color _trueColor;
        [SerializeField] private Color _falseColor;
        
        public void SetValue(bool value) =>
            CachedGraphic.color = value ? _trueColor : _falseColor;
    }
}