using UnityEditor;

namespace Aspid.MVVM.Mono
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoViewModel), editorForChildClasses: true)]
    public class MonoViewModelEditor : ViewModelEditor<MonoViewModel> { }
}