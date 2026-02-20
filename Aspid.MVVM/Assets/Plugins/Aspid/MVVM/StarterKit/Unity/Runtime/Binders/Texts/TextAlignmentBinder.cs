#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class TextAlignmentBinder : TargetBinder<TMP_Text, TextAlignmentOptions>
    {
        protected sealed override TextAlignmentOptions Property
        {
            get => Target.alignment;
            set => Target.alignment = value;
        }
        
        public TextAlignmentBinder(TMP_Text target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif