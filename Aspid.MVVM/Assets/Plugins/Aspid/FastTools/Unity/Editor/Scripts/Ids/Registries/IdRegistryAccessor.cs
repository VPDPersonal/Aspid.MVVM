#nullable enable
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Aspid.FastTools.Ids;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal sealed class IdRegistryAccessor : IRegistryAccessor
    {
        private readonly IdRegistry _registry;
        private readonly SerializedProperty _idsProp;
        private readonly SerializedProperty _namesProp;

        public Object Target => _registry;
        public SerializedObject SerializedObject { get; }
        public SerializedProperty TargetStructTypeProperty { get; }
        public SerializedProperty NextIdProperty { get; }

        public IdRegistryAccessor(IdRegistry registry)
        {
            _registry = registry;
            SerializedObject = new SerializedObject(registry);
            _idsProp = SerializedObject.FindProperty("_ids");
            _namesProp = SerializedObject.FindProperty("_names");
            TargetStructTypeProperty = SerializedObject.FindProperty("_targetStructType");
            NextIdProperty = SerializedObject.FindProperty("_nextId");
        }

        public int Count => Mathf.Min(_idsProp.arraySize, _namesProp.arraySize);

        public int GetId(int index) =>
            _idsProp.GetArrayElementAtIndex(index).intValue;

        public string GetName(int index) =>
            _namesProp.GetArrayElementAtIndex(index).stringValue;

        public int Add(string name)
        {
            var id = NextIdProperty.intValue;
            NextIdProperty.intValue = id + 1;

            var newIndex = _idsProp.arraySize;
            _idsProp.arraySize = newIndex + 1;
            _namesProp.arraySize = newIndex + 1;
            _idsProp.GetArrayElementAtIndex(newIndex).intValue = id;
            _namesProp.GetArrayElementAtIndex(newIndex).stringValue = name;
            return id;
        }

        public void SetName(int index, string name) =>
            _namesProp.GetArrayElementAtIndex(index).stringValue = name;

        public void RemoveAt(int index)
        {
            _idsProp.DeleteArrayElementAtIndex(index);
            if (index < _namesProp.arraySize)
                _namesProp.DeleteArrayElementAtIndex(index);
        }

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
            if (_idsProp.arraySize == _namesProp.arraySize)
            {
                reason = string.Empty;
                return false;
            }
            reason = $"Length mismatch: _ids has {_idsProp.arraySize} entries, _names has {_namesProp.arraySize}.";
            return true;
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
