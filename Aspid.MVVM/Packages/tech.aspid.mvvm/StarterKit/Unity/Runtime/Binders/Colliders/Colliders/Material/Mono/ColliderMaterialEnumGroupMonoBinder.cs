using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.PhysicsMaterial, UnityEngine.PhysicsMaterial>;
#else
using PhysicsMaterial = UnityEngine.PhysicMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverterPhysicsMaterial;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{T1, T2, T3}">EnumGroupMonoBinder&lt;Collider, PhysicsMaterial, IConverter&lt;PhysicsMaterial, PhysicsMaterial&gt;&gt;</see> that sets the <see cref="Collider.material"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Material EnumGroup")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_Material", SubPath = "EnumGroup")]
    public sealed class ColliderMaterialEnumGroupMonoBinder : EnumGroupMonoBinder<Collider, PhysicsMaterial, Converter>
    {
        /// <inheritdoc/>
        protected override void SetValue(Collider element, PhysicsMaterial value) =>
            element.material = value;
    }
}