using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;RawImage, Rect&gt;</see> that binds
    /// <see cref="RawImage.uvRect"/>.
    /// </summary>
    /// <remarks>A non-finite component is refused, since a <c>NaN</c> in any of the four values makes the image vanish.</remarks>
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_UVRect")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – UV Rect")]
    public class RawImageUvRectMonoBinder : ComponentMonoBinder<RawImage, Rect>
    {
        /// <inheritdoc/>
        protected sealed override Rect Property
        {
            get => CachedComponent.uvRect;
            set
            {
                if (!IsFinite(value)) return;
                CachedComponent.uvRect = value;
            }
        }

        private static bool IsFinite(Rect value) =>
            BinderMath.IsFinite(value.x) && BinderMath.IsFinite(value.y)
            && BinderMath.IsFinite(value.width) && BinderMath.IsFinite(value.height);
    }
}
