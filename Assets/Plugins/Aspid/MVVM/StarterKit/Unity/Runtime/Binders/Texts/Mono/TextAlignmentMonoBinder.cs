#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;

namespace Aspid.MVVM.StarterKit.Unity
{
    public partial class TextAlignmentMonoBinder : ComponentMonoBinder<TMP_Text>, IBinder<TextAlignmentOptions>
    {
        [BinderLog]
        public void SetValue(TextAlignmentOptions value) =>
            CachedComponent.alignment = value;
    }
}
#endif