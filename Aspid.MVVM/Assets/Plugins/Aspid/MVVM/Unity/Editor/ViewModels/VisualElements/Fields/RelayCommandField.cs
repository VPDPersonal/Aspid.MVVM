using System;
using System.Linq;
using UnityEditor;
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
    // TODO Aspid.MVVM Unity – Refactor
    // TODO Aspid.MVVM Unity – Write summary
    internal sealed class RelayCommandField : VisualElement
    {
        private const BindingFlags BindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        
        public RelayCommandField(
            object valueContainer, 
            object value,
            Type valueType,
            string valueName, 
            bool isTitle = true,
            Action executeCallback = null, 
            Action notifyCanExecuteChangedCallBack = null)
        {
            var container = new AspidContainer(AspidContainer.StyleType.Lighter);

            if (isTitle)
            {
                container.AddChild(new AspidTitle(ObjectNames.NicifyVariableName(valueName)));
            }
            
            var parameters = BuildFieldsForParameters(valueContainer.GetType(), valueType, valueName);

            var width = new StyleLength(new Length(50, LengthUnit.Percent));
            container.AddChildren(parameters.Select(parameter => parameter.Item2));

            var executeButton = new Button(() =>
            {
                var arguments = new object[parameters.Count];

                for (var i = 0; i < parameters.Count; i++)
                {
                    var (parameterType, field) = parameters[i];
                    arguments[i] = GetValueFromField(field, parameterType);
                }

                var execute = value.GetType().GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance);
                execute!.Invoke(value, arguments);

                executeCallback?.Invoke();
            }).SetText("Execute")
                .SetSize(width)
                .SetMargin(0, 0, 0, 0);

            var notifyCanExecuteChanged = new Button(() =>
            {
                var notifyCanExecuteChanged = value.GetType().GetMethod("NotifyCanExecuteChanged", BindingFlags.Public | BindingFlags.Instance);
                notifyCanExecuteChanged!.Invoke(value, new object[] { });
                
                notifyCanExecuteChangedCallBack?.Invoke();
            }).SetText("Notify Can Execute Changed")
                .SetSize(width)
                .SetMargin(0, 0, 0, 0);
            
            var horizontalContainer = new VisualElement()
                .SetAlignItems(Align.Center)
                .SetFlexDirection(FlexDirection.Row);
            
            horizontalContainer.AddChild(executeButton);
            horizontalContainer.AddChild(notifyCanExecuteChanged);
            container.AddChild(horizontalContainer);
            
            this.AddChild(container);
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
                    parameters.Add((type, field));
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
                .GetMembersInfosIncludingBaseClasses(BindingAttr)
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
            if (typeof(Object).IsAssignableFrom(type)) return new ObjectField(paramName);
            if (typeof(Gradient).IsAssignableFrom(type)) return new GradientField(paramName);
            if (typeof(AnimationCurve).IsAssignableFrom(type)) return new CurveField(paramName);

            return null;
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

            return null;
        }
    }
}