#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

namespace Aspid.MVVM.Mono
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoViewModel), editorForChildClasses: true)]
    public class MonoViewModelEditor : ViewModelEditor<MonoViewModel> { }
}
#endif