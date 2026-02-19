#nullable enable
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class SelectableInteractableBinder : TargetBoolBinder<TMP_Dropdown>
    {
        protected sealed override bool Property
        {
            get => Target.interactable;
            set => Target.interactable = value;
        }

        public SelectableInteractableBinder(TMP_Dropdown target, BindMode mode)
            : this(target, isInvert: false, mode) { }
        
        public SelectableInteractableBinder(TMP_Dropdown target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}