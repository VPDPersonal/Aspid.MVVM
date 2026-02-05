using System.Linq;
using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace Aspid.UnityFastTools.Editors
{
    public static class EditorExtensions
    {
        public static string GetScriptName(this Object obj)
        {
            if (!obj) return string.Empty;
			
            var targetType = obj.GetType();
            var attributes = targetType.GetCustomAttributes(false);

            return attributes.Any(attribute => attribute is AddComponentMenu) 
                ? ObjectNames.GetInspectorTitle(obj)
                : ObjectNames.NicifyVariableName(targetType.Name);
        }

        public static string GetScriptNameWithIndex(this Component targetComponent)
        {
            if (targetComponent is null) return null;
            
            var type = targetComponent.GetType();
            var components = targetComponent.GetComponents(type);
            
            switch (components.Length)
            {
                case 0:
                case 1: return targetComponent.GetScriptName();
                default:
                    {
                        var index = 0;

                        foreach (var component in components)
                        {
                            if (component.GetType() != type) continue;

                            index++;
                            if (component == targetComponent)
                                return $"{targetComponent.GetScriptName()} ({index})";
                        }

                        return targetComponent.GetScriptName();
                    }
            }
        }
    }
}