using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="TweenMonoBinder{T}">TweenMonoBinder&lt;Vector3&gt;</see> that eases a vector toward each value
    /// it receives.
    /// </summary>
    /// <remarks>
    /// A marker that slides to a new position, a panel that moves rather than teleports, a scale that grows. Because this
    /// is a caster, the eased value can drive a position binder, a scale binder or anything else through the event.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/Tween/Tween Binder – Vector3")]
    [AddBinderContextMenuByType(typeof(Vector3))]
    public sealed partial class TweenVector3MonoBinder : TweenMonoBinder<Vector3>
    {
        /// <inheritdoc/>
        protected override Vector3 Interpolate(Vector3 from, Vector3 to, float progress) =>
            Vector3.LerpUnclamped(from, to, progress);
    }
}
