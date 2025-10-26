using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Move To UnityFastTools
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AddPropertyContextMenu : Attribute
    {
        public Type Type { get; }
        
        public string Path { get; set; }
        
        public string SerializePropertyName { get; }

        public AddPropertyContextMenu(Type componentType, string serializePropertyName = null)
        {
            Type = componentType;
            SerializePropertyName = serializePropertyName;
        }
    }
}