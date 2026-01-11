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
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Material")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Raw Image/RawImage Binder – Material")]
    public partial class RawImageMaterialMonoBinder : ComponentMonoBinder<RawImage>, IBinder<Material>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [BinderLog]
        public void SetValue(Material value) =>
            CachedComponent.material = _converter?.Convert(value) ?? value;
    }
}