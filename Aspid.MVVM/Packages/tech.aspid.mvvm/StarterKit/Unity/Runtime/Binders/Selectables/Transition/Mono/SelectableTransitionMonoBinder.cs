using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;Selectable, Selectable.Transition&gt;</see> that binds
    /// <see cref="Selectable.transition"/>.
    /// </summary>
    /// <remarks>
    /// How the control reacts to being hovered or pressed: colour tint, sprite swap, animation, or nothing at
    /// all. Turning it off is how a control is made to look inert without being disabled — the package bound
    /// the colours and left the switch that decides whether they are used.
    /// </remarks>
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Transition")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Transition")]
    public class SelectableTransitionMonoBinder : ComponentMonoBinder<Selectable, Selectable.Transition>
    {
        /// <inheritdoc/>
        protected sealed override Selectable.Transition Property
        {
            get => CachedComponent.transition;
            set => CachedComponent.transition = value;
        }
    }
}
