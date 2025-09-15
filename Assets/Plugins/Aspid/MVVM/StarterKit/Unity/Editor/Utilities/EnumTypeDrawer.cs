using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    internal sealed class EnumTypeDrawer
    {
        private Node _root;
        private Type _enumType;
        
        public void Draw(Rect rect, SerializedProperty enumTypeProperty)
        {
            if (_root is null)
            {
                _root = BuildEnumTree();
                CollapseSingleBranches(_root);
            }

            var buttonLabel = "None";
            var enumTypeString = enumTypeProperty.stringValue;

            if (!string.IsNullOrEmpty(enumTypeString))
            {
                _enumType = Type.GetType(enumTypeString);
                if (_enumType is not null) buttonLabel = _enumType.FullName ?? _enumType.Name;
                else buttonLabel += " (Invalid)";
            }

            if (GUI.Button(rect, buttonLabel, EditorStyles.popup))
                EnumTypeMenu(enumTypeProperty);
        }
        
        private void EnumTypeMenu(SerializedProperty enumTypeProperty)
        {
            var menu = new GenericMenu();
            
            menu.AddItem(new GUIContent("None"), false, () =>
            {
                _enumType = null;
                enumTypeProperty.stringValue = string.Empty;
                enumTypeProperty.serializedObject.ApplyModifiedProperties();
            });
            
            AddMenuItemsRecursive(menu, _root, string.Empty, enumTypeProperty);
            menu.ShowAsContext();
        }
        
        private void AddMenuItemsRecursive(GenericMenu menu, Node node, string path, SerializedProperty enumTypeProperty)
        {
            foreach (var pair in node.Nodes.OrderBy(pair => pair.Key))
            {
                var newPath = string.IsNullOrEmpty(path) 
                    ? pair.Key 
                    : $"{path}/{pair.Key}";
                
                AddMenuItemsRecursive(menu, pair.Value, newPath, enumTypeProperty);
            }

            foreach (var type in node.Types.OrderBy(type => type.Name))
            {
                var itemPath = string.IsNullOrEmpty(path) 
                    ? type.Name 
                    : $"{path}/{type.Name}";
                
                menu.AddItem(new GUIContent(itemPath), false, () =>
                {
                    _enumType = type;
                    enumTypeProperty.stringValue = type.AssemblyQualifiedName;
                    enumTypeProperty.serializedObject.ApplyModifiedProperties();
                });
            }
        }
        
        private static Node BuildEnumTree()
        {
            var root = new Node();
            var enumTypes= TypeCache.GetTypesDerivedFrom<Enum>()
                .Where(type => !type.IsNested);

            foreach (var type in enumTypes)
            {
                var path = (type.Namespace ?? "(Global)").Split('.');

                var current = root;
                foreach (var part in path)
                {
                    var nodes = current.Nodes;
                    
                    if (!nodes.ContainsKey(part))
                        nodes[part] = new Node();
                    
                    current = nodes[part];
                }

                current.Types.Add(type);
            }

            return root;
        }
        
        private static void CollapseSingleBranches(Node node)
        {
            var keys = node.Nodes.Keys.ToArray();
            
            foreach (var key in keys)
            {
                var mergedKey = key;
                var childNode = node.Nodes[key];
                CollapseSingleBranches(childNode);
                
                while (childNode.Types.Count is 0 && childNode.Nodes.Count is 1)
                {
                    var subKey = childNode.Nodes.Keys.First();
                    var subChild = childNode.Nodes[subKey];
            
                    mergedKey += "." + subKey;
                    childNode = subChild;
                }
            
                if (mergedKey != key)
                {
                    node.Nodes.Remove(key);
                    node.Nodes[mergedKey] = childNode;
                }
            }
        }
        
        private sealed class Node
        {
            public List<Type> Types { get; } = new();
            
            public Dictionary<string, Node> Nodes { get; } = new();
        }
    }
}