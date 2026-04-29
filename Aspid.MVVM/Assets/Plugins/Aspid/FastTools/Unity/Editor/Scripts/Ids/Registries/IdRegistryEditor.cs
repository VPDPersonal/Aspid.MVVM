using UnityEditor;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    [CustomEditor(typeof(IdRegistry))]
    internal sealed class IdRegistryEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var accessor = new IdRegistryAccessor((IdRegistry)target);
            return new RegistryEditorCore(accessor).Build();
        }
    }
}
