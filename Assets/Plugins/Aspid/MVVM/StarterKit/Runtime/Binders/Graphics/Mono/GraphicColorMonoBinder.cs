using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Graphic/Graphic Binder - Color")]
    public partial class GraphicColorMonoBinder : ComponentMonoBinder<Graphic>, IColorBinder
    {
        [Header("Converter")]
#if ASPID_MVVM_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<Color, Color> _converter;
#else
        [SerializeReference] private IConverterColorToColor _converter;
#endif
        
        [BinderLog]
        public void SetValue(Color value) =>
            CachedComponent.color = _converter?.Convert(value) ?? value;
    }
}