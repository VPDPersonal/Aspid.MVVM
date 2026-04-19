using UnityEditor;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal static class IdRegisterEditorExtensions
    {
        internal static int Add(this IdRegistry idRegistry, string nameId)
        {
            var registry = new Registry(idRegistry);
            var entries = registry.Entries;

            for (var i = 0; i < entries.Size; i++)
            {
                if (entries[i].Name != nameId) continue;
                return entries[i].Id;
            }
            
            var id = registry.NextId;
            registry.NextId++;

            var lastIndex = entries.Size;
            entries.Size++;
            
            entries[lastIndex].Id = id;
            entries[lastIndex].Name = nameId;

            return id;
        }

        internal static void Rename(this IdRegistry idRegistry, string oldName, string newName)
        {
            var registry = new Registry(idRegistry);
            var entries = registry.Entries;
            
            for (var i = 0; i < entries.Size; i++)
            {
                if (entries[i].Name != oldName) continue;

                entries[i].Name = newName;
                return;
            }
        }

        private class Registry
        {
            private readonly SerializedProperty _nextIdProperty;
            
            public int NextId
            {
                get => _nextIdProperty.intValue;
                set => _nextIdProperty.SetValueAndApply(value);
            }
            
            public Entries Entries { get; }

            public Registry(IdRegistry registry)
            {
                var serializedObject = new SerializedObject(registry);
                
                _nextIdProperty = serializedObject.FindProperty("_nextId");
                Entries = new Entries(serializedObject);
            }
        }

        private class Entries
        {
            private readonly SerializedProperty _property;

            public int Size
            {
                get => _property.arraySize;
                set => _property.SetArraySizeAndApply(value);
            }
            
            public EntryProperty this[int index] => 
                EntryProperty.Create(_property.GetArrayElementAtIndex(index));

            public Entries(SerializedObject serializedObject)
            {
                _property = serializedObject.FindProperty("_entries");
            }
        }
        
        private class EntryProperty
        {
            private readonly SerializedProperty _id;
            private readonly SerializedProperty _name;

            public int Id
            {
                get => _id.intValue;
                set => _id.SetValueAndApply(value);
            }
            
            public string Name
            {
                get => _name.stringValue;
                set => _name.SetValueAndApply(value);
            }
            
            private EntryProperty(SerializedProperty id, SerializedProperty name)
            {
                _id = id;
                _name = name;
            }
            
            public static EntryProperty Create(SerializedProperty property) => new(
                id: property.FindPropertyRelative("Id"), 
                name: property.FindPropertyRelative("Name"));
        }
    }
}
