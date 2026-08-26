using UnityEngine;
using PhysicsMaterial = UnityEngine.PhysicsMaterial;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.PhysicsMaterial, UnityEngine.PhysicsMaterial>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2, T3}">ComponentMonoBinder&lt;Collider, PhysicsMaterial, IConverter&lt;PhysicsMaterial, PhysicsMaterial&gt;&gt;</see> that binds the <see cref="Collider.material"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current material value
    /// is sent back to the ViewModel.
    /// <para/>
    /// The value read back is <see cref="Collider.sharedMaterial"/>, not <see cref="Collider.material"/>: reading
    /// the latter makes Unity replace the assigned asset with a private clone named <c>"… (Instance)"</c> — the
    /// ViewModel would receive something that no longer compares equal to the asset it handed over, and the clone
    /// lives until the collider is destroyed.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Material")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_Material")]
    public class ColliderMaterialMonoBinder : ComponentMonoBinder<Collider, PhysicsMaterial, Converter>
    {
        /// <inheritdoc/>
        protected sealed override PhysicsMaterial Property
        {
            get => CachedComponent.sharedMaterial;
            set => CachedComponent.material = value;
        }
    }
}