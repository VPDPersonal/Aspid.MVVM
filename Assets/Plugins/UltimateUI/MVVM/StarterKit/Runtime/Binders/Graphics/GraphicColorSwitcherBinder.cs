using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Graphics
{
    [AddComponentMenu("UI/Binders/Graphic/Graphic Binder - Color Switcher")]
    public partial class GraphicColorSwitcherBinder : GraphicBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Color _trueColor;
        [SerializeField] private Color _falseColor;

        protected Color TrueColor => _trueColor;
        
        protected Color FalseColor => _falseColor;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedGraphic.color = GetColor(value);
        
        protected Color GetColor(bool value) =>
            value ? TrueColor : FalseColor;
    }
}