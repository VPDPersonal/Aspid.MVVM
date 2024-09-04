using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Graphics
{
    [AddComponentMenu("UI/Binders/Graphic/Graphic Binder - Color")]
    public partial class GraphicColorMonoBinder : ComponentMonoBinder<Graphic>, IColorBinder
    {
        [field: Header("Converter")]
        [field: SerializeReference]
        protected IConverterColorToColor Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(Color value) =>
            CachedComponent.color = Converter?.Convert(value) ?? value;
    }
}