using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Graphic/Graphic Binder - Color")]
    public sealed partial class GraphicColorMonoBinder : ComponentMonoBinder<Graphic>, IColorBinder
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Color, Color> _converter;
#else
        private IConverterColorToColor _converter;
#endif
        
        [BinderLog]
        public void SetValue(Color value) =>
            CachedComponent.color = _converter?.Convert(value) ?? value;
    }
}