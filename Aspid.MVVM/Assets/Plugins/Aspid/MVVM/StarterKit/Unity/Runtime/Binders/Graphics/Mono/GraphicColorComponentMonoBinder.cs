using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{   
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Component")]
    public class GraphicColorComponentMonoBinder : ComponentMonoBinder<Graphic>, INumberBinder
    {
        [SerializeField] private ColorComponent _component = ColorComponent.A;
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        public void SetValue(int value) =>
            SetValue((float)value);

        public void SetValue(long value) =>
            SetValue((float)value);

        public void SetValue(float value) =>
            CachedComponent.SetColor(_component, _converter?.Convert(value) ?? value);

        public void SetValue(double value) =>
            SetValue((float)value);
    }
}
