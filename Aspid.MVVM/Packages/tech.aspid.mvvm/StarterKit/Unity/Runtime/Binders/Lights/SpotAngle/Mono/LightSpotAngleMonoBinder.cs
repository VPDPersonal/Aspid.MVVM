using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Light}"/> that binds <see cref="Light.spotAngle"/>.
    /// </summary>
    /// <remarks>
    /// The width of a spot light's cone, in degrees. Unity keeps it inside 1..179 itself. A non-finite value is
    /// dropped rather than written. Unity clamps the range on its own, so nothing else needs guarding here, but it
    /// stores <see cref="float.NaN"/> verbatim — and a NaN in a rendering number does not fail loudly, it just
    /// makes the image wrong in a way that points nowhere near the ViewModel that produced it.
    /// </remarks>
    [AddBinderContextMenu(typeof(Light), serializePropertyNames: "m_SpotAngle")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Light Binder – Spot Angle")]
    public class LightSpotAngleMonoBinder : ComponentFloatMonoBinder<Light>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.spotAngle;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                CachedComponent.spotAngle = value;
            }
        }
    }
}
