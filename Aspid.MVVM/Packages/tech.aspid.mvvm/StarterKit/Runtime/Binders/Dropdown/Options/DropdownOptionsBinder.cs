#nullable enable
using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that binds <see cref="TMP_Dropdown.options"/> from labels, sprites or option
    /// data.
    /// </summary>
    /// <remarks>
    /// The selection is kept where the new list still has room for it; a selection that no longer fits is clamped
    /// without being reported, that channel belongs to the Value binder.
    /// </remarks>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class DropdownOptionsBinder : TargetBinder<TMP_Dropdown>,
        IBinder<List<string>>,
        IBinder<List<Sprite>>,
        IBinder<IEnumerable<TMP_Dropdown.OptionData>>,
        IReverseBinder<List<TMP_Dropdown.OptionData>>
    {
        /// <param name="target">The dropdown to bind.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public DropdownOptionsBinder(TMP_Dropdown target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        public event Action<List<TMP_Dropdown.OptionData>>? ValueChanged;

        /// <summary>
        /// Replaces the options with labels; <see langword="null"/> clears them.
        /// </summary>
        /// <param name="values">The values received from the ViewModel.</param>
        public void SetValue(List<string>? values)
        {
            var selected = Target.value;

            Target.ClearOptions();
            if (values is not null) Target.AddOptions(values);

            RestoreSelection(selected);
        }

        /// <summary>
        /// Replaces the options with sprites; <see langword="null"/> clears them.
        /// </summary>
        /// <param name="values">The values received from the ViewModel.</param>
        public void SetValue(List<Sprite>? values)
        {
            var selected = Target.value;

            Target.ClearOptions();
            if (values is not null) Target.AddOptions(values);

            RestoreSelection(selected);
        }

        /// <summary>
        /// Replaces the options; <see langword="null"/> clears them.
        /// </summary>
        /// <param name="values">The values received from the ViewModel.</param>
        public void SetValue(IEnumerable<TMP_Dropdown.OptionData>? values)
        {
            var selected = Target.value;

            Target.ClearOptions();

            if (values is not null)
            {
                foreach (var value in values)
                    Target.options.Add(value);
            }

            RestoreSelection(selected);
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(Target.options);
        }

        private void RestoreSelection(int selected)
        {
            if (Target.options.Count > 0)
                Target.SetValueWithoutNotify(Mathf.Clamp(selected, 0, Target.options.Count - 1));

            // Adding to the options list directly does not refresh the caption.
            Target.RefreshShownValue();
        }
    }
}
