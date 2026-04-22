using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SwitcherMonoBinder<>), editorForChildClasses: true)]
    public class SwitcherMonoBinderEditor : MonoBinderEditor
    {
        protected override MonoBinderVisualElement BuildVisualElement() =>
            new SwitcherMonoBinderVisualElement(editor: this);
    }
}
