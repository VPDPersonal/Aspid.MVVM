using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TMP_Dropdown}"/> that binds <see cref="TMP_Dropdown.value"/>.
    /// </summary>
    /// <remarks>
    /// Writes go through <see cref="TMP_Dropdown.SetValueWithoutNotify"/> rather than assigning
    /// <see cref="TMP_Dropdown.value"/> directly, which would raise <see cref="TMP_Dropdown.onValueChanged"/> as
    /// if the user had clicked, echoing the write back to every binder on the dropdown.
    /// <para/>
    /// When Unity clamps the selection to the available options, the clamped value is reported back to the
    /// ViewModel.
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

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="TMP_Dropdown.onValueChanged"/> when using
        /// <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            base.OnBound();

            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            CachedComponent.onValueChanged.AddListener(RaiseNumberValueChanged);
        }

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="TMP_Dropdown.onValueChanged"/>.
        /// </summary>
        protected override void OnUnbound()
        {
            base.OnUnbound();

            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            CachedComponent.onValueChanged.RemoveListener(RaiseNumberValueChanged);
        }
    }
}
