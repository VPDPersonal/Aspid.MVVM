using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{T1, T2}">EnumGroupMonoBinder&lt;Renderer, bool&gt;</see> that sets the <see cref="Renderer.enabled"/>
    /// property on each element based on the bound enum ViewModel value.
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
