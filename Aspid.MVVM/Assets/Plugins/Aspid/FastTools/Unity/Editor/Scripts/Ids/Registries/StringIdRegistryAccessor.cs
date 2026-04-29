#nullable enable
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal sealed class StringIdRegistryAccessor : IRegistryAccessor
    {
        private readonly StringIdRegistry _registry;
        private readonly SerializedProperty _entriesProp;

        public Object Target => _registry;
        public SerializedObject SerializedObject { get; }
        public SerializedProperty TargetStructTypeProperty { get; }
        public SerializedProperty NextIdProperty { get; }

        public StringIdRegistryAccessor(StringIdRegistry registry)
        {
            _registry = registry;
            SerializedObject = new SerializedObject(registry);
            _entriesProp = SerializedObject.FindProperty("_entries");
            TargetStructTypeProperty = SerializedObject.FindProperty("_targetStructType");
            NextIdProperty = SerializedObject.FindProperty("_nextId");
        }

        public int Count => _entriesProp.arraySize;

        public int GetId(int index) =>
            _entriesProp.GetArrayElementAtIndex(index).FindPropertyRelative("Id").intValue;

        public string GetName(int index) =>
            _entriesProp.GetArrayElementAtIndex(index).FindPropertyRelative("Name").stringValue;

        public int Add(string name)
        {
            var id = NextIdProperty.intValue;
            NextIdProperty.intValue = id + 1;

            var newIndex = _entriesProp.arraySize;
            _entriesProp.arraySize = newIndex + 1;
            var element = _entriesProp.GetArrayElementAtIndex(newIndex);
            element.FindPropertyRelative("Id").intValue = id;
            element.FindPropertyRelative("Name").stringValue = name;
            return id;
        }

        public void SetName(int index, string name) =>
            _entriesProp.GetArrayElementAtIndex(index).FindPropertyRelative("Name").stringValue = name;

        public void RemoveAt(int index) =>
            _entriesProp.DeleteArrayElementAtIndex(index);

        public bool Contains(string name)
        {
            for (var i = 0; i < Count; i++)
                if (GetName(i) == name) return true;
            return false;
        }

        public int MaxAssignedId
        {
            get
            {
                var max = 0;
                for (var i = 0; i < Count; i++)
                {
                    var id = GetId(i);
                    if (id > max) max = id;
                }
                return max;
            }
        }

        public void Record(string operationName) =>
            Undo.RegisterCompleteObjectUndo(_registry, operationName);

        public void Commit()
        {
            SerializedObject.ApplyModifiedProperties();
            _registry.InvalidateCache();
            EditorUtility.SetDirty(_registry);
        }

        public bool HasStructuralDamage(out string reason)
        {
            reason = string.Empty;
            return false; // StringIdRegistry has a single-property storage, always consistent.
        }

        public IEnumerable<int> EnumerateInvalidIndices()
        {
            var seen = new HashSet<string>();
            for (var i = 0; i < Count; i++)
            {
                var name = GetName(i);
                if (string.IsNullOrEmpty(name))
                {
                    yield return i;
                    continue;
                }
                if (!seen.Add(name))
                    yield return i;
            }
        }
    }
}
