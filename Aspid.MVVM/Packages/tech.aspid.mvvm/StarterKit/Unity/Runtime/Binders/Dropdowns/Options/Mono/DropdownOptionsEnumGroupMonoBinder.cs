using TMPro;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{T1, T2}">EnumGroupMonoBinder&lt;TMP_Dropdown, List&lt;TMP_Dropdown.OptionData&gt;&gt;</see> that sets the
    /// <see cref="TMP_Dropdown.options"/> list on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Dropdown), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Options EnumGroup")]
    public sealed class DropdownOptionsEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Dropdown, List<TMP_Dropdown.OptionData>>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_Dropdown element, List<TMP_Dropdown.OptionData> value) =>
            element.options = value;
    }
}