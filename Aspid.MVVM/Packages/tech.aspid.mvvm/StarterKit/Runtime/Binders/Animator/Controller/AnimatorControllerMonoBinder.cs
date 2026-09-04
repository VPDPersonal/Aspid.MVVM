using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> that binds
    /// <see cref="Animator.runtimeAnimatorController"/>.
    /// </summary>
    /// <remarks>
    /// Assigning a controller resets the state machine; parameter values are kept.
    /// </remarks>
    [GenerateSerializableBinder]
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
