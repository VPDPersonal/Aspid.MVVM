#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public class DropdownOptionsBinder : TargetBinder<TMP_Dropdown>,
        IBinder<List<string>>,
        IBinder<List<Sprite>>,
        IBinder<IEnumerable<TMP_Dropdown.OptionData>>,
        IReverseBinder<List<TMP_Dropdown.OptionData>>
    {
        public event Action<List<TMP_Dropdown.OptionData>>? ValueChanged;

        public DropdownOptionsBinder(TMP_Dropdown target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
        
        public void SetValue(List<string>? values)
        {
            Target.ClearOptions();
            
            if (values is null) return;
            Target.AddOptions(values);
        }

        public void SetValue(List<Sprite>? values)
        {
            Target.ClearOptions();
            
            if (values is null) return;
            Target.AddOptions(values);
        }
        
        public void SetValue(IEnumerable<TMP_Dropdown.OptionData>? values)
        {
            Target.options ??= new List<TMP_Dropdown.OptionData>();
            Target.options.Clear();
            
            if (values is null) return;
            
            foreach (var value in values)
                Target.options.Add(value);
        }

        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(Target.options);
        }
    }
}
#endif