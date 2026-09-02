using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{T1, T2}">EnumMonoBinder&lt;Renderer, bool&gt;</see> that sets the <see cref="Renderer.enabled"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_Enabled", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Enabled Enum")]
    public sealed class RendererEnabledEnumMonoBinder : EnumMonoBinder<Renderer, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}
