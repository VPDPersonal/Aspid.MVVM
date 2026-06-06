using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SwitcherMonoBinder<,,>), editorForChildClasses: true)]
    internal class SwitcherMonoBinderComponentConverterEditor : SwitcherMonoBinderEditor { }
}
