using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// UIToolkit field that displays a <see cref="Type"/> as a dropdown backed by
    /// <see cref="TypeSelectorWindow"/>, optionally bound to a string-typed
    /// <see cref="SerializedProperty"/> that stores the type's assembly-qualified name.
    /// </summary>
    /// <remarks>
    /// An unresolved assembly-qualified name is preserved and rendered as a
    /// <c>&lt;Missing&gt;</c> caption instead of being silently cleared.
    /// Designed to be inheritable so subclasses (e.g. <see cref="InspectorTypeField"/>) can
    /// layer Inspector-specific styling on top of the base behaviour.
    /// </remarks>
    [UxmlElement]
    public partial class TypeField : BaseField<Type>
    {
        private const string StyleSheetPath = "UI/Types/Aspid-FastTools-SerializableType";

        private readonly Button _openButton;
        private readonly TextElement _textElement;
        private readonly VisualElement _visualInput;
        private readonly SerializedProperty _property;

        private bool _isReadOnly;
        private string _missingAssemblyQualifiedName;

        /// <summary>
        /// Filters which kinds of types can be picked (abstract, interface, …).
        /// </summary>
        [UxmlAttribute]
        public TypeAllow Allow { get; set; } = TypeAllow.None;

        /// <summary>
        /// Base types — the dropdown lists subtypes assignable to every one of them.
        /// </summary>
        public Type[] Types { get; set; } = { typeof(object) };

        /// <summary>
        /// When <see langword="true"/> the dropdown is disabled — the displayed type cannot be
        /// changed — while the open-in-script-editor button stays active.
        /// </summary>
        [UxmlAttribute]
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                _visualInput.SetEnabled(!value);
            }
        }

        public TypeField()
            : this(label: null) { }

        public TypeField(SerializedProperty property)
            : this(property.displayName, property) { }

        public TypeField(string label, SerializedProperty property)
            : this(label)
        {
            _property = property.Persistent();
            SetValueFromAssemblyQualifiedNameWithoutNotify(_property.stringValue);

            // Undo/redo, revert-to-prefab and scripted edits rewrite the backing string outside this field;
            // the tracked callback hands over a fresh property each tick (the Persistent() contract).
            this.TrackPropertyValue(_property, current =>
                SetValueFromAssemblyQualifiedNameWithoutNotify(current.stringValue));
        }

        public TypeField(string label, Type defaultValue = null)
            : this(label, visualInput: new VisualElement(), defaultValue) { }

        private TypeField(string label, VisualElement visualInput, Type defaultValue)
            : base(label, visualInput)
        {
            this.AddClass(EnumField.ussClassName)
                .AddStyleSheetsFromResource(StyleSheetPath)
                .AddAspidThemeStyleSheets();
            
            _visualInput = visualInput;
            
            _textElement = new TextElement()
                .AddClass(EnumField.textUssClassName)
                .SetPickingMode(PickingMode.Ignore);

            visualInput
                .AddClass(EnumField.inputUssClassName)
                .AddChild(_textElement)
                .AddChild(new VisualElement()
                    .AddClass(EnumField.arrowUssClassName)
                    .SetPickingMode(PickingMode.Ignore)
                );
            
            visualInput.RegisterCallback<PointerDownEvent>(OnDropdownClicked);
            
            _openButton = new Button()
                .AddChild(new VisualElement())
                .AddClicked(() => value.OpenInScriptEditor());

            this.AddChild(_openButton);
            SetValueWithoutNotify(defaultValue);
        }

        /// <inheritdoc/>
        public sealed override void SetValueWithoutNotify(Type newValue)
        {
            _missingAssemblyQualifiedName = null;
            base.SetValueWithoutNotify(newValue);
            UpdateDisplay();
        }

        /// <summary>
        /// Sets the field value from an assembly-qualified type name without raising a change event.
        /// </summary>
        /// <remarks>
        /// If the name cannot be resolved to a <see cref="Type"/>, the original string is preserved
        /// so the field can render a <c>&lt;Missing&gt;</c> caption instead of silently clearing.
        /// </remarks>
        public void SetValueFromAssemblyQualifiedNameWithoutNotify(string assemblyQualifiedName)
        {
            var resolved = string.IsNullOrEmpty(assemblyQualifiedName)
                ? null
                : Type.GetType(assemblyQualifiedName, throwOnError: false);

            _missingAssemblyQualifiedName = resolved is null && !string.IsNullOrWhiteSpace(assemblyQualifiedName)
                ? assemblyQualifiedName
                : null;

            base.SetValueWithoutNotify(resolved);
            UpdateDisplay();
        }
        
        private void UpdateDisplay()
        {
            _textElement.SetText(TypeSelectorHelpers.GetTypeSelectorTitle(value, _missingAssemblyQualifiedName));
            _openButton.SetDisplay(value is not null ? DisplayStyle.Flex : DisplayStyle.None);
        }

        private void OnDropdownClicked(PointerDownEvent evt)
        {
            if (_isReadOnly || evt.button is not 0) return;

            var window = _visualInput.GetOwnerWindow();
            if (!window) return;

            var filter = new TypeSelectorFilter
            {
                Types = Types,
                Allow = Allow,
            };

            TypeSelectorWindow.Show(
                screenRect: GetScreenRect(),
                filter: filter,
                currentAqn: value?.AssemblyQualifiedName ?? _missingAssemblyQualifiedName ?? string.Empty,
                onSelected: assemblyQualifiedName =>
                {
                    this.SetValue(string.IsNullOrEmpty(assemblyQualifiedName)
                        ? null
                        : Type.GetType(assemblyQualifiedName, throwOnError: false));

                    // <None> arrives as null (the TypeSelectorWindow contract); store string.Empty like the IMGUI path.
                    _property?.SetStringAndApply(assemblyQualifiedName ?? string.Empty);
                });

            evt.StopPropagation();
            return;

            Rect GetScreenRect() => new(
                window.position.x + _visualInput.worldBound.xMin,
                window.position.y + _visualInput.worldBound.yMin,
                _visualInput.worldBound.width,
                _visualInput.worldBound.height);
        }
    }
}
