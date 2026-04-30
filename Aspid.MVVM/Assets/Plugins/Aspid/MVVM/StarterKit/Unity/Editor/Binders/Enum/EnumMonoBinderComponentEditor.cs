using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [CustomEditor(typeof(EnumMonoBinder<,>), editorForChildClasses: true)]
    internal class EnumMonoBinderComponentEditor : EnumMonoBinderEditor { }
}
