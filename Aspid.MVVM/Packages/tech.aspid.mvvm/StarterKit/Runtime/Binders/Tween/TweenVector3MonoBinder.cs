using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TweenMonoBinder{TValue}"/> that eases a <see cref="Vector3"/>.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddBinderContextMenuByType(typeof(Vector3))]
    [AddComponentMenu("Aspid/MVVM/Binders/Tween/Tween Binder – Vector3")]
    public sealed partial class TweenVector3MonoBinder : TweenMonoBinder<Vector3>, IVector3Binder
    {
        /// <inheritdoc/>
        protected override Vector3 Interpolate(Vector3 from, Vector3 to, float progress) =>
            Vector3.LerpUnclamped(from, to, progress);
    }
}
