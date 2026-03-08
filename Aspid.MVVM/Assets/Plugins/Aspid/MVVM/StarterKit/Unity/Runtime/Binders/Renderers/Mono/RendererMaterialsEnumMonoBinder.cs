using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Material, UnityEngine.Material>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the material color property on a <see cref="Renderer"/> component
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Materials Enum")]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Materials", SubPath = "Enum")]
    public sealed class RendererMaterialsEnumMonoBinder : EnumMonoBinder<Renderer, Material[]>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(Material[] values) =>
            CachedComponent.SetMaterials(_converter, values);
    }
}