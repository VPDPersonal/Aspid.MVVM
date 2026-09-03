using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Graphic}"/> that binds the selected channels of <see cref="Graphic.color"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Channel")]
    public class GraphicColorChannelMonoBinder : ComponentFloatMonoBinder<Graphic>
    {
        [Tooltip("Channels the value writes to; the first selected one is read back.")]
        [SerializeField] private ColorChannels _channels = ColorChannels.A;

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.GetColorChannel(_channels);
            set => CachedComponent.SetColorChannels(_channels, value);
        }
    }
}