using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}">SwitcherMonoBinder&lt;Graphic, float&gt;</see> that switches the selected channels of <see cref="Graphic.color"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder – Color Channel Switcher")]
    [AddBinderContextMenu(typeof(Graphic), serializePropertyNames: "m_Color", SubPath = "Switcher")]
    public sealed class GraphicColorChannelSwitcherMonoBinder : SwitcherMonoBinder<Graphic, float>
    {
        [Tooltip("Channels the value writes to. Several channels take the same value.")]
        [SerializeField] private ColorChannels _channels = ColorChannels.A;

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.SetColorChannels(_channels, value);
    }
}