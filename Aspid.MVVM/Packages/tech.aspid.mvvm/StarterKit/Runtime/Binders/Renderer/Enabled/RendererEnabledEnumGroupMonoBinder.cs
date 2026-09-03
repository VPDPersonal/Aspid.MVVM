using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Renderer.enabled"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Enabled", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Enabled EnumGroup")]
    public sealed class RendererEnabledEnumGroupMonoBinder : EnumGroupMonoBinder<Renderer, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(Renderer element, bool value) =>
            element.enabled = value;
    }
}
