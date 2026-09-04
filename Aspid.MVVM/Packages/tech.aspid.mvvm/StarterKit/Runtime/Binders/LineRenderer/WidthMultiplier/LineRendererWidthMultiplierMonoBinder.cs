using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="LineRenderer.widthMultiplier"/>.
    /// </summary>
    /// <remarks>
    /// A negative value is raised to zero.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "m_Parameters.widthMultiplier")]
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Width Multiplier")]
    public class LineRendererWidthMultiplierMonoBinder : ComponentFloatMonoBinder<LineRenderer>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.widthMultiplier;
            set => CachedComponent.widthMultiplier = this.NonNegative(value);
        }
    }
}
