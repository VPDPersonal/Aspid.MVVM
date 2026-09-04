using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="RectMask2D.padding"/> as
    /// <c>(left, bottom, right)</c>; the top padding keeps its value.
    /// </summary>
    /// <remarks>
    /// A non-finite value is refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RectMask2D), serializePropertyNames: "m_Padding")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectMask2D/RectMask2D Binder – Padding")]
    public class RectMask2DPaddingMonoBinder : ComponentMonoBinder<RectMask2D, Vector3>, IVector3Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => CachedComponent.padding;
            set
            {
                if (!this.RequireFinite(value)) return;

                var padding = CachedComponent.padding;
                CachedComponent.padding = new Vector4(value.x, value.y, value.z, padding.w);
            }
        }
    }
}
