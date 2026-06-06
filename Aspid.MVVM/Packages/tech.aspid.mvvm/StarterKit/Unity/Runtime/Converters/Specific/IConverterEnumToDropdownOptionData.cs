using TMPro;
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public interface IConverterEnumToDropdownOptionData : IConverter<Enum, IEnumerable<TMP_Dropdown.OptionData>> { }
}