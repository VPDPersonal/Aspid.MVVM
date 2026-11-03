using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Scrollbar.value"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..1.
    /// </remarks>
    [AddBinderContextMenu(
        type: typeof(Scrollbar),
        serializePropertyNames: "m_Value",
        SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Scrollbar/Scrollbar Binder – Value Enum")]
    public sealed class ScrollbarValueEnumMonoBinder : EnumMonoBinder<Scrollbar, float>
    {
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Never;

        /// <inheritdoc/>
        protected override void SetValue(float value)
        {
            var clamped = this.SafeClamp01(value);

            if (_notifyMode.ShouldNotify(IsBound)) CachedComponent.value = clamped;
            else CachedComponent.SetValueWithoutNotify(clamped);
        }
    }
}
