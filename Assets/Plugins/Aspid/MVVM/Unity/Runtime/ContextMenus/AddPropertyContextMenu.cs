using System;
using System.Diagnostics;

namespace Aspid.MVVM.Unity
{
    // TODO Move To UnityFastTools
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AddPropertyContextMenu : Attribute
    {
        public Type Type { get; }
        
        public string Path { get; set; }
        
        public string SerializePropertyName { get; }

        public AddPropertyContextMenu(Type propertyType) 
            : this(propertyType, null) { }
        
        public AddPropertyContextMenu(Type componentType, string serializePropertyName)
        {
            Type = componentType;
            SerializePropertyName = serializePropertyName;
        }
    }
}