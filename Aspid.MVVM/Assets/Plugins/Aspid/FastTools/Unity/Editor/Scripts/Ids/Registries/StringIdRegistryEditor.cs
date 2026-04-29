using UnityEditor;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    [CustomEditor(typeof(StringIdRegistry))]
    internal sealed class StringIdRegistryEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var accessor = new StringIdRegistryAccessor((StringIdRegistry)target);
            return new RegistryEditorCore(accessor).Build();
        }
    }
}
