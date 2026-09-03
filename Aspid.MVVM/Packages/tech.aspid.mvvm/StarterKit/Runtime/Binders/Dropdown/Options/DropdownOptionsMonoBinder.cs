using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that binds <see cref="TMP_Dropdown.options"/> from labels, sprites
    /// or option data.
    /// </summary>
    /// <remarks>
    /// The selection is kept where the new list still has room for it; a selection that no longer fits is clamped
    /// without being reported, that channel belongs to the Value binder.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_Options")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Options")]
    public class DropdownOptionsMonoBinder : ComponentMonoBinder<TMP_Dropdown>,
        IBinder<List<string>>,
        IBinder<List<Sprite>>,
        IBinder<IEnumerable<TMP_Dropdown.OptionData>>,
        IReverseBinder<List<TMP_Dropdown.OptionData>>
    {
        /// <inheritdoc/>
        public event Action<List<TMP_Dropdown.OptionData>> ValueChanged;

        /// <summary>
        /// Replaces the options with labels; <see langword="null"/> clears them.
        /// </summary>
        /// <param name="values">The values received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(List<string> values)
        {
            var selected = CachedComponent.value;

            CachedComponent.ClearOptions();
            if (values is not null) CachedComponent.AddOptions(values);

            RestoreSelection(selected);
        }

        /// <summary>
        /// Replaces the options with sprites; <see langword="null"/> clears them.
        /// </summary>
        /// <param name="values">The values received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(List<Sprite> values)
        {
            var selected = CachedComponent.value;

            CachedComponent.ClearOptions();
            if (values is not null) CachedComponent.AddOptions(values);

            RestoreSelection(selected);
        }

        /// <summary>
        /// Replaces the options; <see langword="null"/> clears them.
        /// </summary>
        /// <param name="values">The values received from the ViewModel.</param>
        [BinderLog]
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

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(CachedComponent.options);
        }

        private void RestoreSelection(int selected)
        {
            if (CachedComponent.options.Count > 0)
                CachedComponent.SetValueWithoutNotify(Mathf.Clamp(selected, 0, CachedComponent.options.Count - 1));

            // Adding to the options list directly does not refresh the caption.
            CachedComponent.RefreshShownValue();
        }
    }
}
