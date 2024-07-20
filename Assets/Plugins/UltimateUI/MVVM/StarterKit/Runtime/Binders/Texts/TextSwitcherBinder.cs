#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts
{
    [AddComponentMenu("UI/Binders/Text/Text Binder - Switcher")]
    public partial class TextSwitcherBinder : TextBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private string _trueText;
        [SerializeField] private string _falseText;

        protected string TrueText => _trueText;
        
        protected string FalseText => _falseText;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedText.text = GetText(value);

        protected string GetText(bool value) =>
            value ? TrueText : FalseText;
    }
}
#endif