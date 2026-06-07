using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    /// <summary>
    /// <see cref="IdField"/> variant pre-styled to match a Unity Inspector property row:
    /// applies <see cref="BaseField{TValueType}.alignedFieldUssClassName"/> and the
    /// <see cref="PropertyField"/> USS classes so the label aligns with sibling property fields.
    /// </summary>
    /// <remarks>
    /// Use this in custom property drawers; use <see cref="IdField"/> directly for stand-alone
    /// editor windows where Inspector alignment is not desired.
    /// </remarks>
    [UxmlElement]
    public sealed partial class InspectorIdField : IdField
    {
        public InspectorIdField()
        {
            Initialize();
        }

        public InspectorIdField(SerializedProperty property)
            : base(property)
        {
            Initialize();
        }

        public InspectorIdField(string label, SerializedProperty property)
            : base(label, property)
        {
            Initialize();
        }

        public InspectorIdField(string label, int defaultValue = 0)
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
