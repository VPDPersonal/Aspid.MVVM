using UnityEditor;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    [CustomEditor(typeof(IdRegistry))]
    internal sealed class IdRegistryEditor : Editor
    {
        public override VisualElement CreateInspectorGUI() =>
            new RegistryEditorCore((IdRegistry)target).Build();
    }
}
