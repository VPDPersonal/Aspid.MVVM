using System;
using System.Linq;
using UnityEngine;
using System.Reflection;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugField : VisualElement, IUpdatableDebugField, ISearchableDebugField
    {
        private const string StyleSheetPath = "Styles/Debug/Fields/aspid-debug-field"; 
        
        private readonly string _label;
        private readonly IUpdatableDebugField _updatableField;
        private readonly ISearchableDebugField _searchableField;
        
        public DebugField(object obj, MemberInfo memberInfo, bool isAlternativeColor = false)
            : this(GetLabel(memberInfo), FieldContextFactory.Create(obj, memberInfo, isAlternativeColor)) { }

        public DebugField(string label, IFieldContext context)
        {
            try
            {
                styleSheets.Add(Resources.Load<StyleSheet>(StyleSheetPath));
                
                _label = label;
                var inputField = GetInputField(label, context);
                
                _updatableField = inputField as IUpdatableDebugField;
                _searchableField = inputField as ISearchableDebugField;
                
                Add(Setup(inputField, context));
            }
            catch (Exception e)
            {
                Clear();
                Add(new HelpBox(text: e.ToString(), HelpBoxMessageType.Error));
            }
        }
        
        public void UpdateValue() =>
            _updatableField?.UpdateValue();
        
        public bool Search(string searchPath)
        {
            if (string.IsNullOrEmpty(searchPath))
            {
                ClearSearch();
                return true;
            }
            
            var parts = searchPath.Split(new[] { '.' }, 2);
            var currentSearch = parts[0];
            var remainingPath = parts.Length > 1 ? parts[1] : null;
            var hasDotInQuery = searchPath.Contains('.');
            
            // Check if the current field name matches (partial match, case-insensitive)
            var currentMatches = _label.IndexOf(currentSearch, StringComparison.OrdinalIgnoreCase) >= 0;
            
            if (currentMatches)
            {
                // If a query contains a dot (e.g. "h." or "h.skill"), we're looking for nested fields
                if (hasDotInQuery)
                {
                    // This field matches, but we need nested fields
                    if (_searchableField is not null)
                    {
                        // Pass the remaining path (empty string for "h." means show all nested)
                        var pathToSearch = remainingPath ?? string.Empty;
                        var nestedMatches = _searchableField.Search(pathToSearch);
                        style.display = nestedMatches ? DisplayStyle.Flex : DisplayStyle.None;
                        return nestedMatches;
                    }
                    
                    // Query has a dot, but this field has no nested fields - hide it
                    style.display = DisplayStyle.None;
                    return false;
                }
                
                // Simple query without a dot-show matching field
                style.display = DisplayStyle.Flex;
                return true;
            }
            
            // The current field doesn't match - hide it
            // Don't search nested fields for simple queries (optimization)
            style.display = DisplayStyle.None;
            return false;
        }
        
        public void ClearSearch()
        {
            style.display = DisplayStyle.Flex;
            _searchableField?.ClearSearch();
        }

        private static string GetLabel(MemberInfo memberInfo)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                if (fieldInfo.IsDefined(typeof(BaseBindAttribute))
                    || (fieldInfo.FieldType.IsRelayCommandType() 
                    && fieldInfo.IsDefined(typeof(GeneratedCodeAttribute))
                    && fieldInfo.Name.StartsWith("__")))
                    return fieldInfo.GetGeneratedPropertyName();
            }
            
            return memberInfo.Name;
        }

        private static VisualElement Setup(VisualElement field, IFieldContext context)
        {
            var button = CreateMetaButton();

            var metaContainer = new MetaInfoContainer(context, () =>
            {
                field.SetMargin(right: 0);
                button.SetDisplay(DisplayStyle.Flex);
            });
            
            button.clicked += () =>
            {
                field.SetMargin(right: 5);
                button.SetDisplay(DisplayStyle.None);
                metaContainer.SetDisplay(DisplayStyle.Flex);
            };
            
            var fieldContainer = new VisualElement()
                .SetFlexDirection(FlexDirection.Row);

            var marker = new VisualElement();
            marker.AddToClassList("marker");
            
            if (context.MemberType.IsRelayCommandType())
            {
                marker.AddToClassList("command-marker");
                fieldContainer.AddChild(marker);
            }
            else if (context is BindFieldContext)
            {
                marker.AddToClassList("bind-marker");
                fieldContainer.AddChild(marker);
            }
            
            fieldContainer
                .AddChild(field.SetFlexGrow(1f))
                .AddChild(button);

            var debugFieldContainer = new VisualElement()
                .SetName("debug-field-container")
                .AddChild(metaContainer)
                .AddChild(fieldContainer);
            
            if (context.IsAlternativeColor)
                debugFieldContainer.AddToClassList("alternative-color");

            return debugFieldContainer;

            Button CreateMetaButton() => new Button().SetName("meta-button").SetText("▲");
        }
        
        private static VisualElement GetInputField(string label, IFieldContext context)
        {
            var type = context.MemberType;
            
            if (typeof(byte) == type) return new DebugByteField(label, context);
            if (typeof(sbyte) == type) return new DebugSbyteField(label, context);
            if (typeof(short) == type) return new DebugShortField(label, context);
            if (typeof(ushort) == type) return new DebugUshortField(label, context);
            if (typeof(int) == type) return new DebugIntegerField(label, context);
            if (typeof(uint) == type) return new DebugUintField(label, context);
            if (typeof(long) == type) return new DebugLongField(label, context);
            if (typeof(ulong) == type) return new DebugUlongField(label, context);
            if (typeof(float) == type) return new DebugFloatField(label, context);
            if (typeof(double) == type) return new DebugDoubleField(label, context);
            if (typeof(decimal) == type) return new DebugDecimalField(label, context);
            if (typeof(bool) == type) return new DebugBoolField(label, context);
            if (typeof(char) == type) return new DebugCharField(label, context);
            if (typeof(string) == type) return new DebugStringField(label, context);
            if (typeof(Color) == type) return new DebugColorField(label, context);
            if (typeof(Rect) == type) return new DebugRectField(label, context);
            if (typeof(RectInt) == type) return new DebugRectIntField(label, context);
            if (typeof(Bounds) == type) return new DebugBoundsField(label, context);
            if (typeof(BoundsInt) == type) return new DebugBoundsIntField(label, context);
            if (typeof(Vector2) == type) return new DebugVector2Field(label, context);
            if (typeof(Vector3) == type) return new DebugVector3Field(label, context);
            if (typeof(Vector4) == type) return new DebugVector4Field(label, context);
            if (typeof(Vector2Int) == type) return new DebugVector2IntField(label, context);
            if (typeof(Vector3Int) == type) return new DebugVector3IntField(label, context);
            if (typeof(Type) == type) return new DebugTypeField(label, context);
            if (typeof(Enum).IsAssignableFrom(type)) return new DebugEnumField(label, context);
            if (typeof(Object).IsAssignableFrom(type)) return new DebugUnityObjectField(label, context);
            if (typeof(Delegate).IsAssignableFrom(type)) return new DebugDelegateField(label, context);
            if (IsViewModel(type)) return new DebugViewModelField(label, context);
            if (type.IsRelayCommandType()) return new DebugRelayCommandField(label, context);
            if (typeof(Gradient).IsAssignableFrom(type)) return new DebugGradientField(label, context);
            if (typeof(AnimationCurve).IsAssignableFrom(type)) return new DebugAnimationCurveField(label, context);
            if (IsCollection(type)) return new DebugEnumerableField(label, context);
            return new DebugCompositeField(label, context);
        }

        private static bool IsViewModel(Type type)
        {
            return type.GetInterfaces()
                .Any(i => i== typeof(IViewModel));
        }

        private static bool IsCollection(Type type)
        {
            if (type.IsArray) return true;
            
            return type.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>));
        }
    }
}