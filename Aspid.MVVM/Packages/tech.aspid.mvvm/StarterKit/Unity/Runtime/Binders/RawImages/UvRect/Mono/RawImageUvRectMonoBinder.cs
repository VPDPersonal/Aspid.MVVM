using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;RawImage, Rect&gt;</see> that binds
    /// <see cref="RawImage.uvRect"/>.
    /// </summary>
    /// <remarks>
    /// Which part of the texture is shown, and how many times it repeats. It is how a scrolling background, a sprite
    /// sheet frame or a minimap window is driven from a value — and the only property of a RawImage worth binding
    /// beyond its texture, which is why the component had no binder at all before.
    /// <para/>
    /// A non-finite component is refused: the quad's UVs are computed from these four numbers and one <c>NaN</c> makes
    /// the image vanish.
    /// <para/>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current rect is sent back to
    /// the ViewModel.
    /// </remarks>
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
