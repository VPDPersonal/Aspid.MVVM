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
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Material Switcher")]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Material", SubPath = "Switcher")]
    public sealed class RawImageMaterialSwitcherMonoBinder : SwitcherMonoBinder<RawImage, Material, Converter>
    {
        protected override void SetValue(Material value) =>
            CachedComponent.material = value;
    }
}