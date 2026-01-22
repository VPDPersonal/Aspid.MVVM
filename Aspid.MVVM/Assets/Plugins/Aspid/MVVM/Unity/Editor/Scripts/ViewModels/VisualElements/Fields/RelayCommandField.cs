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
    public class RelayCommandField : VisualElement
    {
        private const string StyleSheetPath = "Styles/Fields/aspid-relay-command";
        private const BindingFlags BindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        
        public RelayCommandField(string label, object target, FieldInfo fieldInfo)
        {
            styleSheets.Add(styleSheet: Resources.Load<StyleSheet>(StyleSheetPath));
            Build(label, target, fieldInfo);
        }

        private void Build(string label, object target, FieldInfo fieldInfo)
        {
            var value = fieldInfo.GetValue(target);
            
            var container = new VisualElement()
                .SetName("aspid-relay-command-container")
                .AddChild(new Label(label).SetName("label"));

            if (value is null)
            {
                var generatedProperty = target.FindRelayCommandGeneratedProperty(fieldInfo);

                if (generatedProperty is not null)
                {
                    var initializeButton = new Button(clickEvent: () =>
                    {
                        generatedProperty.GetValue(target);
                            
                        Clear();
                        Build(label, target, fieldInfo);
                    });
                    
                    initializeButton
                        .SetName("initialize-button")
                        .SetText("Initialize");
                    
                    container.SetFlexDirection(FlexDirection.Row)
                        .AddChild(initializeButton);
                }
                
                Add(container);
                return;
            }

            var parameters = BuildFieldsForParameters(target, fieldInfo);
            container.AddChildren(parameters.Select(parameter => parameter.Item2));

            var executeButton = new Button(() =>
            {
                var arguments = new object[parameters.Count];
                
                for (var i = 0; i < parameters.Count; i++)
                {
                    var (parameterType, field) = parameters[i];
                    arguments[i] = GetValueFromField(field, parameterType);
                }
                
                value.GetType().GetMethod(name: "Execute", BindingAttr)
                    ?.Invoke(value, arguments);
            }).SetText("Execute");
            
            var notifyButton = new Button(() =>
            {
                value.GetType().GetMethod("NotifyCanExecuteChanged", BindingAttr)
                    ?.Invoke(value, Array.Empty<object>());
            }).SetText("Notify Can Execute Changed");
            
            var horizontalContainer = new VisualElement()
                .SetName("aspid-relay-command-horizontal-container")
                .AddChild(executeButton)
                .AddChild(notifyButton);
            
            Add(container.AddChild(horizontalContainer));
        }
        
        private static List<(Type, VisualElement)> BuildFieldsForParameters(object target, FieldInfo fieldInfo)
        {
            var parameters = new List<(Type, VisualElement)>();
            var methodParameters = GetMethod()?.GetParameters();

            if (fieldInfo.FieldType.IsGenericType)
            {
                var parameterTypes = fieldInfo.FieldType.GenericTypeArguments;
                
                for (var i = 0; i < parameterTypes.Length; i++)
                {
                    var type = parameterTypes[i];
                    var field = BuildFieldByParameter(GetParameterName(i), type);

                    if (field is not null)
                        parameters.Add((type, field));
                }
            }

            return parameters;

            string GetParameterName(int index) => methodParameters is not null 
                    ? methodParameters[index].Name 
                    : $"param {index + 1}";

            MethodInfo GetMethod()
            {
                var method = target.FindCommandMethodByName(fieldInfo);
                return method?.GetParameters().Length is 0 ? null : method;
            }
        }
        
        private static VisualElement BuildFieldByParameter(string paramName, Type type)
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

            return new TextField(paramName)
            {
                isReadOnly = true, 
                value = $"Unsupported type: {type.Name}"
            };
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