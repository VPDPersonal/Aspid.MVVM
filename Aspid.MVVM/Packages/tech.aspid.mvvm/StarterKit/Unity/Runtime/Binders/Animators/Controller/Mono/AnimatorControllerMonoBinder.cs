using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{T1, T2}">ComponentObjectMonoBinder&lt;Animator, RuntimeAnimatorController&gt;</see> that binds
    /// <see cref="Animator.runtimeAnimatorController"/>.
    /// </summary>
    /// <remarks>
    /// Swapping the whole controller is how one rig plays a different set of animations: a character that changes class
    /// or mount, a weapon that brings its own moves. The alternative is a prefab per set.
    /// <para/>
    /// Assigning a controller rebinds the animator and resets its state machine — parameters keep their values, the
    /// state does not. A destroyed controller arrives as <see langword="null"/>, which leaves the animator without one
    /// rather than pointing at an asset that no longer exists.
    /// <para/>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current controller is sent back
    /// to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(Animator), serializePropertyNames: "m_Controller")]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Controller")]
    public class AnimatorControllerMonoBinder : ComponentObjectMonoBinder<Animator, RuntimeAnimatorController>
    {
        /// <inheritdoc/>
        protected sealed override RuntimeAnimatorController Property
        {
            get => CachedComponent.runtimeAnimatorController;
            set => CachedComponent.runtimeAnimatorController = value;
        }
    }
}
