#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TMP_Dropdown}"/> that manages the <see cref="TMP_Dropdown.options"/>
    /// list when the bound ViewModel value changes.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Dropdown))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Options")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class DropdownOptionsMonoBinder : ComponentMonoBinder<TMP_Dropdown>,
        IBinder<List<string>>,
        IBinder<List<Sprite>>,
        IBinder<IEnumerable<TMP_Dropdown.OptionData>>,
        IReverseBinder<List<TMP_Dropdown.OptionData>>
    {
        /// <inheritdoc/>
        public event Action<List<TMP_Dropdown.OptionData>> ValueChanged;

        /// <inheritdoc/>
        public void SetValue(List<string> values)
        {
            var selected = CachedComponent.value;

            CachedComponent.ClearOptions();
            if (values is not null) CachedComponent.AddOptions(values);

            RestoreSelection(selected);
        }

        /// <inheritdoc/>
        public void SetValue(List<Sprite> values)
        {
            var selected = CachedComponent.value;

            CachedComponent.ClearOptions();
            if (values is not null) CachedComponent.AddOptions(values);

            RestoreSelection(selected);
        }

        /// <inheritdoc/>
        public void SetValue(IEnumerable<TMP_Dropdown.OptionData> values)
        {
            var selected = CachedComponent.value;

            CachedComponent.ClearOptions();

            if (values is not null)
            {
                foreach (var value in values)
                    CachedComponent.options.Add(value);
            }

            RestoreSelection(selected);
        }


        /// <summary>
        /// Rebuilds the option list while keeping the current selection where the new list still has room for it.
        /// </summary>
        /// <remarks>
        /// <see cref="TMP_Dropdown.ClearOptions"/> resets the selection without raising a notification; restoring it
        /// here keeps the ViewModel in sync. If the new list is shorter and the selection actually changes, this
        /// binder does not report it — that value channel is <c>DropdownValueMonoBinder</c>.
        /// </remarks>
        private void RestoreSelection(int selected)
        {
            var dropdown = CachedComponent;

            if (dropdown.options.Count > 0)
                dropdown.SetValueWithoutNotify(Mathf.Clamp(selected, 0, dropdown.options.Count - 1));

            // Mutating the options list directly leaves the caption showing whatever it showed before.
            dropdown.RefreshShownValue();
        }

        /// <summary>
        /// Called after binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, propagates the current <see cref="TMP_Dropdown.options"/> list to the ViewModel.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(CachedComponent.options);
        }
    }
}
#endif