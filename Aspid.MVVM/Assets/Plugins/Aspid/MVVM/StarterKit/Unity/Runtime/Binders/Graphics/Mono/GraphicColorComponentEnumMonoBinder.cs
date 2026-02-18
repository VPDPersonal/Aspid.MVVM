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
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Component Enum")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "Enum")]
    public sealed class GraphicColorComponentEnumMonoBinder : EnumMonoBinder<Graphic, float, Converter>
    {
        [SerializeField] private ColorComponent _colorComponent = ColorComponent.A;
        
        protected override void SetValue(float value) =>
            CachedComponent.SetColorComponent(_colorComponent, value);
    }
}