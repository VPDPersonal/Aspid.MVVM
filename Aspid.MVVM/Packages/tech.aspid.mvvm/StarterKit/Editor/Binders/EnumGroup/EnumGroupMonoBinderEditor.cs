using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(EnumGroupMonoBinder<>), editorForChildClasses: true)]
    internal class EnumGroupMonoBinderEditor : MonoBinderEditor
    {
        protected override MonoBinderVisualElement BuildVisualElement() =>
            new EnumGroupMonoBinderVisualElement(editor: this);
    }
}
