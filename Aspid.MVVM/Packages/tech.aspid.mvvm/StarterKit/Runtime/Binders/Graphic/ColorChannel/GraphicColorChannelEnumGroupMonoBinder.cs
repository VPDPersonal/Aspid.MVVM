using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}">EnumGroupMonoBinder&lt;Graphic, float&gt;</see> that sets the selected channels of <see cref="Graphic.color"/> per group element.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Channel EnumGroup")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "EnumGroup")]
    public sealed class GraphicColorChannelEnumGroupMonoBinder : EnumGroupMonoBinder<Graphic, float>
    {
        [Tooltip("Channels the value writes to. Several channels take the same value.")]
        [SerializeField] private ColorChannels _channels = ColorChannels.A;

        /// <inheritdoc/>
        protected override void SetValue(Graphic element, float value) =>
            element.SetColorChannels(_channels, value);
    }
}