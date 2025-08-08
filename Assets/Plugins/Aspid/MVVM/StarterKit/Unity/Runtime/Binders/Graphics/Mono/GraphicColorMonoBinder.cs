using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterColor;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Graphic), "m_Color")]
    [AddPropertyContextMenu(typeof(Graphic), "m_Color")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder - Color")]
    [AddComponentContextMenu(typeof(Graphic),"Add Graphic Binder/Graphic Binder - Color")]
    public partial class GraphicColorMonoBinder : ComponentMonoBinder<Graphic>, IColorBinder
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [BinderLog]
        public void SetValue(Color value) =>
            CachedComponent.color = _converter?.Convert(value) ?? value;
    }
}