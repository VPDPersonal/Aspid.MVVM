using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that fills <see cref="TMP_Dropdown.options"/> with the values of
    /// the bound enum type.
    /// </summary>
    /// <remarks>
    /// The options depend on the enum type, not the value, so they are rebuilt only when the type changes;
    /// <see langword="null"/> clears them. The selection is kept where the new list still has room for it.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_Options")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Options By Enum")]
    public partial class DropdownOptionsByEnumMonoBinder : ComponentMonoBinder<TMP_Dropdown>, IBinder<Enum>
    {
        [Tooltip("Optional converter from the enum to option data; empty uses the value names.")]
        [SerializeReference] private IConverter<Enum, IEnumerable<TMP_Dropdown.OptionData>> _converter;

        private Type _populatedType;

        /// <inheritdoc/>
        [BinderLog]
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
                    dropdown.options.Add(new TMP_Dropdown.OptionData(name));
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
