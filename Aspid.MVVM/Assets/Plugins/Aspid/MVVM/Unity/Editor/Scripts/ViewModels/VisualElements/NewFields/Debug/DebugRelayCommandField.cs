using System;
using System.Linq;
using UnityEngine;
using System.Reflection;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugRelayCommandField : VisualElement
    {
        private const BindingFlags BindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        
        internal DebugRelayCommandField(string label, IFieldContext context)
        {
            var commandValue = context.GetValue();
            var commandType = context.MemberType;
            
            Add(BuildCommandField(context.Target, commandValue, commandType, label));
        }

        private static VisualElement BuildCommandField(object valueContainer, object commandValue, Type commandType, string label)
        {
            var container = new VisualElement()
                .SetPadding(4)
                .SetBorderRadius(4);

            container.AddChild(new Label(label)
                .SetMargin(bottom: 4));

            if (commandValue is null)
            {
                container.AddChild(new Label("null")
                    .SetColor(new Color(0.6f, 0.6f, 0.6f)));
                return container;
            }

            var parameters = BuildFieldsForParameters(valueContainer.GetType(), commandType, label);
            container.AddChildren(parameters.Select(parameter => parameter.Item2));

            var width = new StyleLength(new Length(50, LengthUnit.Percent));
            
            var executeButton = new Button(() =>
            {
                var arguments = new object[parameters.Count];

                for (var i = 0; i < parameters.Count; i++)
                {
                    var (parameterType, field) = parameters[i];
                    arguments[i] = GetValueFromField(field, parameterType);
                }

                var execute = commandValue.GetType().GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance);
                execute?.Invoke(commandValue, arguments);
            })
                .SetText("Execute")
                .SetSize(width: width)
                .SetMargin(0);

            var notifyButton = new Button(() =>
            {
                var notifyMethod = commandValue.GetType().GetMethod("NotifyCanExecuteChanged", BindingFlags.Public | BindingFlags.Instance);
                notifyMethod?.Invoke(commandValue, Array.Empty<object>());
            }).SetText("Notify Can Execute Changed")
                .SetSize(width: width)
                .SetMargin(0);
            
            var horizontalContainer = new VisualElement()
                .SetAlignItems(Align.Center)
                .SetFlexDirection(FlexDirection.Row)
                .SetMargin(top: 4);
            
            horizontalContainer.AddChild(executeButton);
            horizontalContainer.AddChild(notifyButton);
            container.AddChild(horizontalContainer);
            
            return container;
        }
        
        private static List<(Type, VisualElement)> BuildFieldsForParameters(Type containerType, Type valueType, string valueName)
        {
            var parameters = new List<(Type, VisualElement)>();
            var methods = GetMethods();
            
            if (valueType.IsGenericType)
            {
                var parameterTypes = valueType.GenericTypeArguments;
                    
                for (var i = 0; i < parameterTypes.Length; i++)
                {
                    var type = parameterTypes[i];
                    var field = BuildFieldByParameter(type, GetParameterName(i));
                    if (field is not null)
                    {
                        parameters.Add((type, field));
                    }
                }
            }

            return parameters;

            string GetParameterName(int index)
            {
                return methods.TryGetValue(valueName, out var methodParameters) 
                    ? methodParameters[index].Name 
                    : $"param {index + 1}";
            }
            
            Dictionary<string, ParameterInfo[]> GetMethods() => containerType
                .GetMembersInfosIncludingBaseClasses(bindingFlags: BindingAttr)
                .OfType<MethodInfo>()
                .Where(method => method.GetParameters().Length > 0)
                .Where(method => method.IsDefined(typeof(RelayCommandAttribute)))
                .ToDictionary(method => $"{method.Name}Command", method => method.GetParameters());
        }
        
        private static VisualElement BuildFieldByParameter(Type type, string paramName)
        {
            if (typeof(int) == type) return new IntegerField(paramName);
            if (typeof(long) == type) return new LongField(paramName);
            if (typeof(float) == type) return new FloatField(paramName);
            if (typeof(double) == type) return new DoubleField(paramName);
            if (typeof(bool) == type) return new Toggle(paramName);
            if (typeof(string) == type) return new TextField(paramName);
            if (typeof(Color) == type) return new ColorField(paramName);
            if (typeof(Rect) == type) return new RectField(paramName);
            if (typeof(RectInt) == type) return new RectIntField(paramName);
            if (typeof(Bounds) == type) return new BoundsField(paramName);
            if (typeof(BoundsInt) == type) return new BoundsIntField(paramName);
            if (typeof(Vector2) == type) return new Vector2Field(paramName);
            if (typeof(Vector3) == type) return new Vector3Field(paramName);
            if (typeof(Vector4) == type) return new Vector4Field(paramName);
            if (typeof(Vector2Int) == type) return new Vector2IntField(paramName);
            if (typeof(Vector3Int) == type) return new Vector3IntField(paramName);
            if (typeof(Object).IsAssignableFrom(type)) return new ObjectField(paramName) { objectType = type };
            if (typeof(Gradient).IsAssignableFrom(type)) return new GradientField(paramName);
            if (typeof(AnimationCurve).IsAssignableFrom(type)) return new CurveField(paramName);
            if (typeof(Enum).IsAssignableFrom(type))
            {
                if (type.IsDefined(typeof(FlagsAttribute), inherit: false))
                    return new EnumFlagsField(paramName, Enum.GetValues(type).GetValue(index: 0) as Enum);
                
                return new EnumField(paramName, Enum.GetValues(type).GetValue(index: 0) as Enum);
            }

            return new TextField(paramName) { isReadOnly = true, value = $"Unsupported type: {type.Name}" };
        }
            
        private static object GetValueFromField(VisualElement field, Type type)
        {
            if (typeof(int) == type) return ((IntegerField)field).value;
            if (typeof(long) == type) return ((LongField)field).value;
            if (typeof(float) == type) return ((FloatField)field).value;
            if (typeof(double) == type) return ((DoubleField)field).value;
            if (typeof(bool) == type) return ((Toggle)field).value;
            if (typeof(string) == type) return ((TextField)field).value;
            if (typeof(Color) == type) return ((ColorField)field).value;
            if (typeof(Rect) == type) return ((RectField)field).value;
            if (typeof(RectInt) == type) return ((RectIntField)field).value;
            if (typeof(Bounds) == type) return ((BoundsField)field).value;
            if (typeof(BoundsInt) == type) return ((BoundsIntField)field).value;
            if (typeof(Vector2) == type) return ((Vector2Field)field).value;
            if (typeof(Vector3) == type) return ((Vector3Field)field).value;
            if (typeof(Vector4) == type) return ((Vector4Field)field).value;
            if (typeof(Vector2Int) == type) return ((Vector2IntField)field).value;
            if (typeof(Vector3Int) == type) return ((Vector3IntField)field).value;
            if (typeof(Object).IsAssignableFrom(type)) return ((ObjectField)field).value;
            if (typeof(Gradient).IsAssignableFrom(type)) return ((GradientField)field).value;
            if (typeof(AnimationCurve).IsAssignableFrom(type)) return ((CurveField)field).value;
            if (typeof(Enum).IsAssignableFrom(type))
            {
                if (field is EnumField fieldAsEnum) return fieldAsEnum.value;
                return ((EnumFlagsField)field).value;
            }

            return null;
        }
    }
}
