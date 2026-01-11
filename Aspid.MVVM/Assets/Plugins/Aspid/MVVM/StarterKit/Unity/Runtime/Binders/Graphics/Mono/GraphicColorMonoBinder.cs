using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color")]
    public partial class GraphicColorMonoBinder : ComponentMonoBinder<Graphic>, IColorBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [BinderLog]
        public void SetValue(Color value) =>
            CachedComponent.color = _converter?.Convert(value) ?? value;
    }
}