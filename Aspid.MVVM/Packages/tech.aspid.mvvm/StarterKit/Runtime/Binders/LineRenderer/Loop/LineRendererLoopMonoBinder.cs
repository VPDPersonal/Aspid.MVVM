using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="LineRenderer.loop"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "m_Loop")]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Loop")]
    public class LineRendererLoopMonoBinder : ComponentMonoBinder<LineRenderer, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.loop;
            set => CachedComponent.loop = value;
        }
    }
}
