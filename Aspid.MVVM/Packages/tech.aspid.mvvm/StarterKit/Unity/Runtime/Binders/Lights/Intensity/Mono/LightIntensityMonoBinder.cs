using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder<Light>"/> that binds <see cref="Light.intensity"/>.
    /// </summary>
    /// <remarks>
    /// How bright the lamp burns. A non-finite value is dropped rather than written. Unity clamps the range on its
    /// own, so nothing else needs guarding here, but it stores <see cref="float.NaN"/> verbatim — and a NaN in a
    /// rendering number does not fail loudly, it just makes the image wrong in a way that points nowhere near the
    /// ViewModel that produced it.
    /// </remarks>
    [AddBinderContextMenu(typeof(Light), serializePropertyNames: "m_Intensity")]
    [AddComponentMenu("Aspid/MVVM/Binders/Rendering/Light Binder - Intensity")]
    public class LightIntensityMonoBinder : ComponentFloatMonoBinder<Light>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.intensity;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                CachedComponent.intensity = value;
            }
        }
    }
}
