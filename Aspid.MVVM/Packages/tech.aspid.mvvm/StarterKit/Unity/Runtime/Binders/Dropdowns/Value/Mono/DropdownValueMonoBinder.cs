#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TMP_Dropdown}"/> that binds <see cref="TMP_Dropdown.value"/>.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.TwoWay"/> and <see cref="BindMode.OneWayToSource"/>: when
    /// <see cref="TMP_Dropdown.onValueChanged"/> fires, the current value is forwarded to the ViewModel.
    /// <para/>
    /// A write goes through <see cref="TMP_Dropdown.SetValueWithoutNotify"/>. Assigning
    /// <see cref="TMP_Dropdown.value"/> directly raises <see cref="TMP_Dropdown.onValueChanged"/> exactly as a
    /// click does, so the binder would read its own write back as a user choice — and any other binder on the
    /// same dropdown would too.
    /// <para/>
    /// Unity clamps the selection to the options that exist. When the clamp changes the value, the ViewModel is
    /// told what the dropdown actually holds rather than being left believing in an index that was refused.
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
#endif
