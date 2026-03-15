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
            CachedComponent.ClearOptions();

            if (values is null) return;
            CachedComponent.AddOptions(values);
        }

        /// <inheritdoc/>
        public void SetValue(List<Sprite> values)
        {
            CachedComponent.ClearOptions();

            if (values is null) return;
            CachedComponent.AddOptions(values);
        }

        /// <inheritdoc/>
        public void SetValue(IEnumerable<TMP_Dropdown.OptionData> values)
        {
            CachedComponent.ClearOptions();

            if (values is null) return;

            foreach (var value in values)
                CachedComponent.options.Add(value);
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