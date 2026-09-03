using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}">EnumMonoBinder&lt;Graphic, float&gt;</see> that sets the selected channels of <see cref="Graphic.color"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Channel Enum")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "Enum")]
    public sealed class GraphicColorChannelEnumMonoBinder : EnumMonoBinder<Graphic, float>
    {
        [Tooltip("Channels the value writes to. Several channels take the same value.")]
        [SerializeField] private ColorChannels _channels = ColorChannels.A;

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.SetColorChannels(_channels, value);
    }
}