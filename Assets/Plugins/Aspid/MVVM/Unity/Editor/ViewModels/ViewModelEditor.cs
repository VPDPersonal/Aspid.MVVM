using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Aspid.CustomEditors;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using bindingFlags = System.Reflection.BindingFlags;

namespace Aspid.MVVM.Unity
{
    public abstract class ViewModelEditor<T> : Editor
        where T : Object, IViewModel
    {
        protected const bindingFlags BindingFlags = bindingFlags.Public | bindingFlags.NonPublic | bindingFlags.Instance | bindingFlags.Static;

        protected T ViewModel => target as T;
        
        protected VisualElement Root { get; private set; }
        
        protected string IconPath => MessageType switch
        {
            ErrorType.None => "Aspid Icon",
            ErrorType.Error => "Aspid Icon Red",
            ErrorType.Warning => "Aspid Icon Yellow",
            _ => throw new ArgumentOutOfRangeException()
        };
        
        protected virtual ErrorType MessageType => ErrorType.None;
        
        protected virtual string[] PropertiesExcluding => new[]
        {
            "m_Script",
        };
        
        public sealed override VisualElement CreateInspectorGUI()
        {
            Root = Build();
            return Root;
        }

        protected virtual VisualElement Build()
        {
            return new VisualElement()
                .AddChild(BuildHeader())
                .AddChild(BuildBaseInspector())
                .AddChild(new CommandsContainerBuilder(ViewModel).Build());
        }
        
        protected VisualElement BuildHeader() 
        {
            var header = Elements.CreateHeader(IconPath, GetScriptName());
            header.Q<Image>("HeaderIcon").AddOpenScriptCommand(target);

            return header;
        }
        
        protected VisualElement BuildBaseInspector()
        {
            return Elements.CreateContainer(EditorColor.LightContainer)
                .SetMargin(top: 10)
                .SetName("BaseInspector")
                .AddChild(new IMGUIContainer(DrawBaseInspector));
        }

        private void DrawBaseInspector()
        {
            var propertiesCount = 0;

            serializedObject.UpdateIfRequiredOrScript();
            {
                propertiesCount += OnDrawingBaseInspector();
                
                var enterChildren = true;
                var iterator = serializedObject.GetIterator();
                
                while (iterator.NextVisible(enterChildren))
                {
                    enterChildren = false;
                    if (PropertiesExcluding.Contains(iterator.name)) continue;
                    
                    propertiesCount++;
                    EditorGUILayout.PropertyField(iterator, true);
                }

                propertiesCount += OnDrewBaseInspector();
            }
            serializedObject.ApplyModifiedProperties();
            
            Root.Q<VisualElement>("BaseInspector").style.display = propertiesCount is 0 
                ? DisplayStyle.None 
                : DisplayStyle.Flex;
        }
        
        protected virtual int OnDrawingBaseInspector() => 0;

        protected virtual int OnDrewBaseInspector() => 0;

        protected virtual string GetScriptName() =>
            !ViewModel ? null : ViewModel.GetScriptName();
        
        protected class CommandsContainerBuilder
        {
            private readonly Type _type;
            private readonly string _prefsKey;
            private readonly IViewModel _viewModel;
            
            public CommandsContainerBuilder(IViewModel viewModel)
            {
                _viewModel = viewModel;
                _type = viewModel.GetType();
                _prefsKey = _type + "Commands";
            }

            public VisualElement Build()
            {
                var foldout = new Foldout()
                {
                    text = "Commands",
                    value = EditorPrefs.GetBool(_prefsKey, true)
                }
                .SetMargin(left: 10);
                
                foldout.RegisterValueChangedCallback(e =>
                {
                    if (e.target != foldout) return;
                    
                    if (!string.IsNullOrWhiteSpace(_prefsKey))
                        EditorPrefs.SetBool(_prefsKey, e.newValue);
                });
                
                var container = Elements.CreateContainer(EditorColor.LightContainer)
                    .SetName("Commands")
                    .SetMargin(top: 10)
                    .AddChild(foldout);
                
                var properties = _type.GetMembersInfosIncludingBaseClasses(BindingFlags)
                    .OfType<PropertyInfo>()
                    .Where(property =>
                    {
                        var propertyType = property.PropertyType;
                        if (typeof(IRelayCommand).IsAssignableFrom(propertyType)) return true;
                        
                        var interfaces = new List<Type>(propertyType.GetInterfaces());
                        if (propertyType.IsInterface) interfaces.Add(propertyType);
                        
                        return interfaces.Any(i =>
                            {
                                if (!i.IsGenericType) return false;
                                var genericTypeDefinition = i.GetGenericTypeDefinition();
                                
                                return genericTypeDefinition == typeof(IRelayCommand<>)
                                    || genericTypeDefinition == typeof(IRelayCommand<,>)
                                    || genericTypeDefinition == typeof(IRelayCommand<,,>)
                                    || genericTypeDefinition == typeof(IRelayCommand<,,,>);
                            });
                    });

                var i = 0;
                
                foreach (var property in properties)
                {
                    var command = BuildCommand(property);
                    if (i++ is not 0) command.SetMargin(top: 10);
                    foldout.AddChild(command);
                }

                return container;
            }

            private VisualElement BuildCommand(PropertyInfo property)
            {
                var container = Elements.CreateContainer(EditorColor.LighterContainer);
                var parameters = BuildFieldsForParameters(property);

                container.AddChildren(parameters.Select(parameter => parameter.Item2));
                
                var button = new Button(() =>
                {
                    var arguments = new object[parameters.Count];
                    var propertyValue = property.GetValue(_viewModel);
                    
                    for (var i = 0; i < parameters.Count; i++)
                    {
                        var (parameterType, field) = parameters[i];
                        arguments[i] = GetValueFromField(field, parameterType);
                    }
                    
                    var execute = property.PropertyType.GetMethod("Execute",  BindingFlags.Public | BindingFlags.Instance);
                    execute!.Invoke(propertyValue, arguments);
                })
                {
                    text = ObjectNames.NicifyVariableName(property.Name)
                };

                return container.AddChild(button);
            }

            private List<(Type, VisualElement)> BuildFieldsForParameters(PropertyInfo property)
            {
                var parameters = new List<(Type, VisualElement)>();
                var methods = GetMethods();
                
                if (property.PropertyType.IsGenericType)
                {
                    var parameterTypes = property.PropertyType.GenericTypeArguments;
                    
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
                    return methods.TryGetValue(property.Name, out var methodParameters) 
                        ? methodParameters[index].Name 
                        : $"param {index + 1}";
                }

                Dictionary<string, ParameterInfo[]> GetMethods() => _type
                    .GetMembersInfosIncludingBaseClasses(BindingFlags)
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
}