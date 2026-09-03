using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TComponent}"/> that binds <see cref="TMP_Dropdown.value"/>.
    /// </summary>
    /// <remarks>
    /// Writes use <see cref="TMP_Dropdown.SetValueWithoutNotify"/> so the write is not echoed through
    /// <see cref="TMP_Dropdown.onValueChanged"/>. A selection clamped by the dropdown is reported back.
    /// </remarks>
    [BindModeOverride(IsAll = true)]
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_Value")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Value")]
    public class DropdownValueMonoBinder : ComponentIntMonoBinder<TMP_Dropdown>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.value;
            set
            {
                CachedComponent.SetValueWithoutNotify(value);

                var applied = CachedComponent.value;
                if (applied != value) RaiseNumberValueChanged(applied);
            }
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            base.OnBound();

            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource)
                CachedComponent.onValueChanged.AddListener(RaiseNumberValueChanged);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            base.OnUnbound();

            if (Mode is BindMode.TwoWay or BindMode.OneWayToSource)
                CachedComponent.onValueChanged.RemoveListener(RaiseNumberValueChanged);
        }
    }
}
