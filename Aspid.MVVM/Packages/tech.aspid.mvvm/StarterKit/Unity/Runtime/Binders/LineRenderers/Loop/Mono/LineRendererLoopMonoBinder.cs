using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{LineRenderer}"/> that binds <see cref="LineRenderer.loop"/>.
    /// </summary>
    /// <remarks>
    /// Whether the last point connects back to the first — the difference between a path and an outline, which
    /// is what a selection ring or a closed zone needs.
    /// </remarks>
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "m_Loop")]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Loop")]
    public class LineRendererLoopMonoBinder : ComponentBoolMonoBinder<LineRenderer>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.loop;
            set => CachedComponent.loop = value;
        }
    }
}
