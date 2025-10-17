#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public class TextAlignmentBinder : TargetBinder<TMP_Text>, IBinder<TextAlignmentOptions>
    {
        public TextAlignmentBinder(TMP_Text target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
        }

        public void SetValue(TextAlignmentOptions value) =>
            Target.alignment = value;
    }
}
#endif