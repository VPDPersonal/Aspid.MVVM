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
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color EnumGroup")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "EnumGroup")]
    public sealed class GraphicColorEnumGroupMonoBinder : EnumGroupMonoBinder<Graphic, Color, Converter>
    {
        protected override void SetValue(Graphic element, Color value) =>
            element.color = value;
    }
}