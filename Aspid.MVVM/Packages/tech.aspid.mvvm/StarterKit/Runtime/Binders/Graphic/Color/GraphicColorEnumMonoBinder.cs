using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Graphic.color"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Enum")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "Enum")]
    public sealed class GraphicColorEnumMonoBinder : EnumMonoBinder<Graphic, Color>
    {
        /// <inheritdoc/>
        protected override void SetValue(Color value) =>
            CachedComponent.color = value;
    }
}