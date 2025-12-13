using System;
using System.Linq;
using UnityEngine;
using System.Reflection;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugField : VisualElement, IUpdatableField
    {
        private readonly IUpdatableField _updatableField;
        
        internal DebugField(object obj, MemberInfo memberInfo, bool isAlternativeColor = false)
        {
            var label = GetLabel(memberInfo);
            var context = FieldContextFactory.Create(obj, memberInfo, isAlternativeColor);

            var inputField = GetInputField(label, context);
            _updatableField = inputField as IUpdatableField;
            Add(Settings(inputField, context));

            this.SetMargin(top: 2, bottom: 2);
        }

        internal DebugField(string label, IFieldContext context)
        {
            var inputField = GetInputField(label, context);
            _updatableField = inputField as IUpdatableField;
            Add(Settings(inputField, context));
            
            this.SetMargin(top: 2, bottom: 2);
        }
        
        public void UpdateValue() =>
            _updatableField?.UpdateValue();

        private static string GetLabel(MemberInfo memberInfo)
        {
            if (memberInfo is FieldInfo fieldInfo && fieldInfo.IsDefined(typeof(BaseBindAttribute)))
            {
               return fieldInfo.GetGeneratedPropertyName();
            }
            
            return memberInfo.Name;
        }

        private static VisualElement Settings(VisualElement field, IFieldContext context)
        {
            var button = CreateToggleButton();
            
            var metaContainer = new MetaInfoContainer(context, () =>
                {
                    field.SetMargin(right: 0);
                    button.SetDisplay(DisplayStyle.Flex);
                })
                .SetBorderRadius(5)
                .SetPadding(5)
                .SetBorderWidth(1)
                .SetBorderColor(Color.gray1)
                .SetBackgroundColor(Color.gray3)
                .SetMargin(top: 2, bottom: 5, left: 2, right: 2);
            
            button.clicked += () =>
            {
                field.SetMargin(right: 5);
                button.SetDisplay(DisplayStyle.None);
                metaContainer.SetDisplay(DisplayStyle.Flex);
            };
            
            var fieldContainer = new VisualElement().SetFlexDirection(FlexDirection.Row);

            if (context is BindFieldContext) 
                fieldContainer.AddChild(CreateBindMarker());
            
            fieldContainer
                .AddChild(field.SetFlexGrow(1f))
                .AddChild(button);
            
            return new VisualElement()
                .SetPadding(2)
                .SetBorderRadius(5)
                .SetBackgroundColor(context.IsAlternativeColor
                    ? new Color(0.18f, 0.18f, 0.18f, 1)
                    : new Color(0.22f, 0.22f, 0.22f, 1))
                .AddChild(metaContainer)
                .AddChild(fieldContainer);
            
            Button CreateToggleButton() => new Button()
                .SetText("▲")
                .SetMargin(left: 6)
                .SetSize(width: 24, height: 20);
            
            VisualElement CreateBindMarker() => new VisualElement()
                .SetFlexShrink(0)
                .SetSize(width: 4)
                .SetMargin(left: 2, right: 5)
                .SetBackgroundColor(new Color(0.05f, 0.55f, 0.37f, 1f));
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
            if (typeof(Gradient).IsAssignableFrom(type)) return new DebugGradientField(label, context);
            if (typeof(AnimationCurve).IsAssignableFrom(type)) return new DebugAnimationCurveField(label, context);
            if (IsCollection(type)) return new DebugEnumerableField(label, context);
            return new DebugCompositeField(label, context);
        }

        private static bool IsCollection(Type type)
        {
            if (type.IsArray) return true;
            
            return type.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>));
        }
    }
}
