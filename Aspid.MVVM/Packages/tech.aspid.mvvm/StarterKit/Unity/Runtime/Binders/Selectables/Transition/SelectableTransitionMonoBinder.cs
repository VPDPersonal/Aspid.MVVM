using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}">ComponentMonoBinder&lt;Selectable, Selectable.Transition&gt;</see> that binds
    /// <see cref="Selectable.transition"/>.
    /// </summary>
    [GenerateSerializableBinder]
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
