#nullable enable
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="PropertyField"/> styled for Aspid MVVM Inspectors.
    /// </summary>
    public class AspidPropertyField : PropertyField
    {
        /// <summary>
        /// The style class applied to the field.
        /// </summary>
        public const string StyleClass = "aspid-property-field";
        private const string ImguiFoldoutStyleClass = "aspid-property-field-imgui-foldout";
        /// <summary>
        /// The shared property field style sheet.
        /// </summary>
        public static readonly StyleSheet StyleSheet = Resources.Load<StyleSheet>("Styles/Aspid-MVVM-AspidPropertyField");

        private SerializedProperty? _property;

        /// <summary>
        /// Creates a styled field using the property display name.
        /// </summary>
        /// <param name="property">The property to edit.</param>
        public AspidPropertyField(SerializedProperty property)
            : base(property)
        {
            Initialize(property);
        }

        /// <summary>
        /// Creates a styled field with a custom label.
        /// </summary>
        /// <param name="property">The property to edit.</param>
        /// <param name="label">The field label.</param>
        public AspidPropertyField(SerializedProperty property, string label)
            : base(property, label)
        {
            Initialize(property);
        }

        private void Initialize(SerializedProperty property)
        {
            // Copy so external iteration (e.g. NextVisible in the caller) can't move our iterator past end.
            _property = property.Copy();
            this.Bind(property.serializedObject);

            styleSheets.Add(StyleSheet);
            RegisterCallback<GeometryChangedEvent>(SetStyles);
        }
        
        private void SetStyles(GeometryChangedEvent e)
        {
            // For Field
            var field = Children().FirstOrDefault(element => !element.ClassListContains("unity-decorator-drawers-container"));

            var wrapper = (VisualElement)this;
            while (wrapper.parent is not null and not AspidBox)
                wrapper = wrapper.parent;

            if (wrapper.parent is AspidBox container && container[0] == wrapper)
            {
                Children()
                    .FirstOrDefault(element => element.ClassListContains("unity-decorator-drawers-container"))
                    ?.Children()
                    .FirstOrDefault(element => element.ClassListContains("unity-header-drawer__label"))
                    ?.SetMargin(top: 0);
            }
            
            // IMGUI drawers (e.g. AssetReferenceSprite) are hosted in an IMGUIContainer that
            // paints over its own UIElements background, hiding the Aspid frame. Wrap the
            // IMGUIContainer in a styled VisualElement so the frame renders around the IMGUI
            // content, while keeping any decorator drawers (e.g. [Header]) above the frame –
            // matching the layout of non-IMGUI fields.
            if (field is IMGUIContainer imgui)
            {
                var index = IndexOf(imgui);
                imgui.RemoveFromHierarchy();

                var imguiWrapper = new VisualElement();
                imguiWrapper.AddToClassList(StyleClass);
                imguiWrapper.AddToClassList(ThemeStyle.LightnessClass);
                
                if (_property is not null && IsImguiFoldoutDrawer(_property))
                    imguiWrapper.AddToClassList(ImguiFoldoutStyleClass);
                
                imguiWrapper.Add(imgui);
                Insert(index, imguiWrapper);
            }
            else
            {
                field?.AddToClassList(StyleClass);
                field?.AddToClassList(ThemeStyle.LightnessClass);
            }
        }
        
        // A foldout-style IMGUI drawer (e.g. Localization StringReference) reports a different
        // height when isExpanded flips because it lays children out below the foldout arrow.
        // Single-line drawers (e.g. AssetReferenceSprite) return the same height either way.
        private static bool IsImguiFoldoutDrawer(SerializedProperty property)
        {
            var wasExpanded = property.isExpanded;
            
            try
            {
                property.isExpanded = false;
                var collapsed = EditorGUI.GetPropertyHeight(property);

                property.isExpanded = true;
                var expanded = EditorGUI.GetPropertyHeight(property);

                return expanded > collapsed + 5f;
            }
            finally
            {
                property.isExpanded = wasExpanded;
            }
        }
    }
}