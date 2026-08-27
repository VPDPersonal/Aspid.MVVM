using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [CustomEditor(typeof(EnumMonoBinderWithConverter<,>), editorForChildClasses: true)]
    internal class EnumMonoBinderComponentConverterEditor : EnumMonoBinderEditor { }
}
