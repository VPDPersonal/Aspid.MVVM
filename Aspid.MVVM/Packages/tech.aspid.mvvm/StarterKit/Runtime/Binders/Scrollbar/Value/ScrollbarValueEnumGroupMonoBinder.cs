using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Scrollbar.value"/> on each element.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..1.
    /// </remarks>
    [AddBinderContextMenu(
        type: typeof(Scrollbar),
        serializePropertyNames: "m_Value",
        SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Value EnumGroup")]
    public sealed class ScrollbarValueEnumGroupMonoBinder : EnumGroupMonoBinder<Scrollbar, float>
    {
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Never;

        /// <inheritdoc/>
        protected override void SetValue(Scrollbar element, float value)
        {
            var clamped = this.SafeClamp01(value);

            if (_notifyMode.ShouldNotify(IsBound)) element.value = clamped;
            else element.SetValueWithoutNotify(clamped);
        }
    }
}
