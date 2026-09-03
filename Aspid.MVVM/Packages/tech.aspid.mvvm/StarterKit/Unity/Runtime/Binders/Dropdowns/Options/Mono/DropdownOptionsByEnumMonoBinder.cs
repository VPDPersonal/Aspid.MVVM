using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TMP_Dropdown}"/> that populates <see cref="TMP_Dropdown.options"/>
    /// automatically from the values of a bound enum type.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Dropdown))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Options By Enum")]
    public class DropdownOptionsByEnumMonoBinder : ComponentMonoBinder<TMP_Dropdown>, IBinder<Enum>
    {
        [Tooltip("Converts the enum value to option data. Null uses each value's name.")]
        [SerializeReference] private IConverter<Enum, IEnumerable<TMP_Dropdown.OptionData>> _converter;

        private Type _populatedType;

        /// <summary>
        /// Repopulates <see cref="TMPro.TMP_Dropdown.options"/> from all values of the enum type of
        /// <paramref name="value"/>, using the configured converter when assigned, or the name of each
        /// enum value otherwise.
        /// </summary>
        /// <param name="value">The bound enum value received from the ViewModel. Pass <see langword="null"/> to clear all options.</param>
        /// <remarks>
        /// The option set depends on the enum <i>type</i>, not the value, so it rebuilds only when the type
        /// changes. Options are cleared directly rather than via <see cref="TMPro.TMP_Dropdown.ClearOptions"/>,
        /// which would reset the selected index and disrupt a value binder on the same dropdown.
        /// </remarks>
        public void SetValue(Enum value)
        {
            if (value is null)
            {
                _populatedType = null;
                CachedComponent.ClearOptions();
                return;
            }

            var type = value.GetType();
            var dropdown = CachedComponent;

            if (_populatedType == type && dropdown.options.Count > 0) return;
            _populatedType = type;

            var selected = dropdown.value;
            dropdown.options.Clear();

            if (_converter is null)
            {
                foreach (var name in Enum.GetNames(type))
                    dropdown.options.Add(new TMP_Dropdown.OptionData(text: name));
            }
            else
            {
                foreach (var option in _converter.Convert(value))
                    dropdown.options.Add(option);
            }

            if (dropdown.options.Count > 0)
                dropdown.SetValueWithoutNotify(Mathf.Clamp(selected, 0, dropdown.options.Count - 1));

            dropdown.RefreshShownValue();
        }
    }
}