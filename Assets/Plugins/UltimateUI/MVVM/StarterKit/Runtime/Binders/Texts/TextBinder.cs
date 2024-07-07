#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts
{
    public partial class TextBinder : TextBinderBase, IBinder<string>, IBinderNumber
    {
        [SerializeField] private string _format;

        protected string Format => _format;

        [BinderLog]
        public void SetValue(string value) =>
            CachedText.text = string.IsNullOrEmpty(Format) ? value : string.Format(Format, value);

        public void SetValue(int value) =>
            ((IBinder<string>)this).SetValue(value.ToString());

        public void SetValue(long value) =>
            ((IBinder<string>)this).SetValue(value.ToString());

        public void SetValue(float value) =>
            ((IBinder<string>)this).SetValue(value.ToString(CultureInfo.InvariantCulture));

        public void SetValue(double value) =>
            ((IBinder<string>)this).SetValue(value.ToString(CultureInfo.InvariantCulture));
    }
}
#endif