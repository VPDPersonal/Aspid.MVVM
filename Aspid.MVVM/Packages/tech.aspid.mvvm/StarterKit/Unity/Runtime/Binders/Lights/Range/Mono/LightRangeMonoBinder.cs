using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder<Light>"/> that binds <see cref="Light.range"/>.
    /// </summary>
    /// <remarks>
    /// How far a point or spot light reaches; a directional light ignores it. Unity maps a non-finite range to
    /// zero, which switches the lamp off — dropping the write keeps the last range that lit something instead.
    /// </remarks>
    [AddBinderContextMenu(typeof(Light), serializePropertyNames: "m_Range")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Light Binder – Range")]
    public class LightRangeMonoBinder : ComponentFloatMonoBinder<Light>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.range;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                CachedComponent.range = value;
            }
        }
    }
}
