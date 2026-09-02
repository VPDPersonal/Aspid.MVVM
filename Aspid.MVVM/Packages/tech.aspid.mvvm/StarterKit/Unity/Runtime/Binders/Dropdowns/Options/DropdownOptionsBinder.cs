#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_Dropdown}"/> that manages the <see cref="TMP_Dropdown.options"/> list.
    /// </summary>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class DropdownOptionsBinder : TargetBinder<TMP_Dropdown>,
        IBinder<List<string>>,
        IBinder<List<Sprite>>,
        IBinder<IEnumerable<TMP_Dropdown.OptionData>>,
        IReverseBinder<List<TMP_Dropdown.OptionData>>
    {
        /// <param name="target">The <see cref="TMP_Dropdown"/> whose options will be bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public DropdownOptionsBinder(TMP_Dropdown target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Raised when the bound value changes.
        /// </summary>
        public event Action<List<TMP_Dropdown.OptionData>>? ValueChanged;

        /// <summary>
        /// Clears the current options and replaces them with options built from the bound string list.
        /// Does nothing beyond clearing when <paramref name="values"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="values">The list of string labels to populate as options. Pass <see langword="null"/> to clear all options.</param>
        public void SetValue(List<string>? values)
        {
            var selected = Target.value;

            Target.ClearOptions();
            if (values is not null) Target.AddOptions(values);

            RestoreSelection(selected);
        }

        /// <summary>
        /// Clears the current options and replaces them with options built from the bound sprite list.
        /// Does nothing beyond clearing when <paramref name="values"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="values">The list of sprites to populate as options. Pass <see langword="null"/> to clear all options.</param>
        public void SetValue(List<Sprite>? values)
        {
            var selected = Target.value;

            Target.ClearOptions();
            if (values is not null) Target.AddOptions(values);

            RestoreSelection(selected);
        }

        /// <summary>
        /// Clears the current options and replaces them with the provided <see cref="TMP_Dropdown.OptionData"/> entries.
        /// Does nothing beyond clearing when <paramref name="values"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="values">The option data entries to populate. Pass <see langword="null"/> to clear all options.</param>
        public void SetValue(IEnumerable<TMP_Dropdown.OptionData>? values)
        {
            var selected = Target.value;

            Target.options ??= new List<TMP_Dropdown.OptionData>();
            Target.options.Clear();

            if (values is not null)
            {
                foreach (var value in values)
                    Target.options.Add(value);
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
            if (Target.options.Count > 0)
                Target.SetValueWithoutNotify(Mathf.Clamp(selected, 0, Target.options.Count - 1));

            // Mutating the options list directly leaves the caption showing whatever it showed before.
            Target.RefreshShownValue();
        }

        /// <summary>
        /// Called after binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, propagates the current <see cref="TMP_Dropdown.options"/> list to the ViewModel.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(Target.options);
        }
    }
}
#endif