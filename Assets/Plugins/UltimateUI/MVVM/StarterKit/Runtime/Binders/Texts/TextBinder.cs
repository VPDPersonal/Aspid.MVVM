#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using UnityEngine;
using System.Globalization;
using UltimateUI.MVVM.Unity.Generation;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts
{
    [AddComponentMenu("UI/Binders/Text/Text Binder")]
    public partial class TextBinder : TextBinderBase, IBinder<string>, INumberBinder
    {
        [SerializeField] private string _format;

        protected string Format => _format;
        
        // TODO ZString
        [BinderLog]
        public void SetValue(string value) =>
            CachedText.text = string.IsNullOrEmpty(Format) ? value : string.Format(Format, value);
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToString());
                
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToString());
        
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
                
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
    }
}
#endif