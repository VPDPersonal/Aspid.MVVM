using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> that binds <see cref="Selectable.targetGraphic"/>.
    /// </summary>
    /// <remarks>
    /// A destroyed graphic arrives as <see langword="null"/> and leaves the control untinted.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_TargetGraphic")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Target Graphic")]
    public class SelectableTargetGraphicMonoBinder : ComponentObjectMonoBinder<Selectable, Graphic>
    {
        /// <inheritdoc/>
        protected sealed override Graphic Property
        {
            get => CachedComponent.targetGraphic;
            set => CachedComponent.targetGraphic = value;
        }
    }
}
