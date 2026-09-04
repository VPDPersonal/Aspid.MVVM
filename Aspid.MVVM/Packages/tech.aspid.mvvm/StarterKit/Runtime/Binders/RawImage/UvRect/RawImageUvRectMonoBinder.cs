using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="RawImage.uvRect"/>.
    /// </summary>
    /// <remarks>
    /// A rect with a non-finite component is refused.
    /// </remarks>
    [GenerateSerializableBinder]
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
                if (this.RequireFinite(value))
                    CachedComponent.uvRect = value;
            }
        }
    }
}
