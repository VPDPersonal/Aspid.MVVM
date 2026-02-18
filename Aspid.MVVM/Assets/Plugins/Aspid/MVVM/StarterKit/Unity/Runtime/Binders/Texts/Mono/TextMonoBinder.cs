#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Text")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public partial class TextMonoBinder : ComponentMonoBinder<TMP_Text, string, Converter>, INumberBinder
    {
        protected sealed override string Property
        {
            get => CachedComponent.text;
            set => CachedComponent.text = value;
        }

        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
                
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
        
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
                
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
    }
}
#endif