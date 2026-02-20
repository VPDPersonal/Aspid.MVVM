using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Material EnumGroup")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Material", SubPath = "EnumGroup")]
    public sealed class GraphicMaterialEnumGroupMonoBinder : EnumGroupMonoBinder<Graphic, Material, Converter>
    {
        protected override void SetValue(Graphic element, Material value) =>
            element.material = value;
    }
}