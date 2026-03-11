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
    /// <see cref="ComponentMonoBinder{Collider, PhysicsMaterial, IConverter{PhysicsMaterial, PhysicsMaterial}}"/> that binds the <see cref="Collider.material"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current shared mesh value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Material")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_Material")]
    public class ColliderMaterialMonoBinder : ComponentMonoBinder<Collider, PhysicsMaterial, Converter>
    {
        /// <inheritdoc/>
        protected sealed override PhysicsMaterial Property
        {
            get => CachedComponent.material;
            set => CachedComponent.material = value;
        }
    }
}