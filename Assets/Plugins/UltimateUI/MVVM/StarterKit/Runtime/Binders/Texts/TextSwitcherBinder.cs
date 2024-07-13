#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts
{
    public partial class TextSwitcherBinder : TextBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private string _trueText;
        [SerializeField] private string _falseText;

        protected string TrueText => _trueText;
        
        protected string FalseText => _falseText;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(bool value) =>
            CachedText.text = GetText(value);

        protected string GetText(bool value) =>
            value ? TrueText : FalseText;
    }
}
#endif