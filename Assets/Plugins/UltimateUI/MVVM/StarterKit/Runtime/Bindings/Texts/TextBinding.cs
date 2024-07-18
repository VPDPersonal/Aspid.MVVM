#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Texts
{
    public partial class TextBinding : TextBinderBase, ITargetBinding<string>, INumberTargetBinding
    {
        [SerializeField] private string _format;

        protected string Format => _format;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(string value) =>
            CachedText.text = string.IsNullOrEmpty(Format) ? value : string.Format(Format, value);
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(int value) =>
            SetValue(value.ToString());
                
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(long value) =>
            SetValue(value.ToString());
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(float value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
                
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(double value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
    }
}
#endif