using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SwitcherMonoBinderWithConverter<,>), editorForChildClasses: true)]
    internal class SwitcherMonoBinderComponentConverterEditor : SwitcherMonoBinderEditor { }
}
