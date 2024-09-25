using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Graphics
{
    [AddComponentMenu("UI/Binders/Graphic/Graphic Binder - Color")]
    public partial class GraphicColorMonoBinder : ComponentMonoBinder<Graphic>, IColorBinder
    {
        [field: Header("Converter")]
        [field: SerializeReference]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterColorToColor Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(Color value) =>
            CachedComponent.color = Converter?.Convert(value) ?? value;
    }
}