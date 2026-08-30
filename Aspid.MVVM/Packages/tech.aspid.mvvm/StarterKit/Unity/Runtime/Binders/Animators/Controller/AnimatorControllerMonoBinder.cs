using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{T1, T2}">ComponentObjectMonoBinder&lt;Animator, RuntimeAnimatorController&gt;</see>
    /// that binds <see cref="Animator.runtimeAnimatorController"/>.
    /// </summary>
    /// <remarks>
    /// Assigning a controller rebinds the animator and resets its state machine — parameters keep their values, the
    /// state does not. A destroyed controller arrives as <see langword="null"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Controller")]
    [AddBinderContextMenu(typeof(Animator), serializePropertyNames: "m_Controller")]
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
