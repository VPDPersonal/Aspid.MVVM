using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{RectMask2D, Vector3}"/> that binds <see cref="RectMask2D.padding"/>.
    /// </summary>
    /// <remarks>
    /// The property is a <see cref="Vector4"/>; the fourth component keeps its previous value since only
    /// <see cref="Vector3"/> is bound. Non-finite components are refused.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RectMask2D), serializePropertyNames: "m_Padding")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Mask/RectMask2D Binder – Padding")]
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
