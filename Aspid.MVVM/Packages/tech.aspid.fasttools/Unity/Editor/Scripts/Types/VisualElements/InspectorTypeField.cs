using System;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// <see cref="TypeField"/> variant pre-styled to match a Unity Inspector property row:
    /// applies <see cref="BaseField{TValueType}.alignedFieldUssClassName"/> and the
    /// <see cref="PropertyField"/> USS classes so the label aligns with sibling property fields.
    /// </summary>
    /// <remarks>
    /// Use this in custom property drawers; use <see cref="TypeField"/> directly for
    /// stand-alone editor windows where Inspector alignment is not desired.
    /// </remarks>
    [UxmlElement]
    public sealed partial class InspectorTypeField : TypeField
    {
        public InspectorTypeField()
        {
            Initialize();
        }

        public InspectorTypeField(SerializedProperty property)
            : base(property)
        {
            Initialize();
        }

        public InspectorTypeField(string label, SerializedProperty property)
            : base(label, property)
        {
            Initialize();
        }

        public InspectorTypeField(string label, Type defaultValue = null) 
            : base(label, defaultValue)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.AddClass(alignedFieldUssClassName)
                .AddClass(PropertyField.ussClassName);
            
            labelElement.AddClass(PropertyField.labelUssClassName);
        }
    }
}
