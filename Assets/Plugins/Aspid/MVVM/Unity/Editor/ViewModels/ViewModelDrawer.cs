using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections;
using Aspid.CustomEditors;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.Unity
{
    public static class ViewModelDrawer
    {
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        
        public static VisualElement CreateViewModelContainer(Object target)
        {
            var root = new VisualElement();
            
            var type = target.GetType();
            var property = type.GetProperty("ViewModel", Flags);
            
            root.AddChild(BuildCompositeValue(new ViewModelMemberInfo(target, property),"View Model"));
            return root;
        }

        private static VisualElement BuildCompositeValue(ViewModelMemberInfo member, string label)
        {
            var root = new VisualElement();
            var value = member.Value;
            
            if (value is null)
                return root.AddChild(BuildNullField(label));

            var type = value.GetType();
            return root.AddChild(Foldout(label, member.Name + label, type, DataContainer));

            VisualElement DataContainer()
            {
                var childElement = new VisualElement();

                var baseType = value switch
                {
                    ScriptableObject => typeof(ScriptableObject),
                    MonoBehaviour => typeof(MonoBehaviour),
                    _ => typeof(object)
                };
                
                var fields = type.GetFieldInfosIncludingBaseClasses(Flags, baseType);
                var properties = type.GetPropertyInfosIncludingBaseClasses(Flags, baseType);

                var bindNames = new HashSet<string>();
                var context = new ViewModelFieldsUpdater();

                foreach (var field in fields)
                {
                    if (field.GetCustomAttribute<BaseBindAttribute>() is not null)
                    {
                        bindNames.Add(field.GetGeneratedPropertyName());
                        continue;
                    }

                    childElement.AddChild(BuildField(context, new ViewModelMemberInfo(value, field)));
                }

                foreach (var property in properties)
                {
                    if (property.GetIndexParameters().Length > 0) continue;
                    
                    if (type.GetExplicitInterface(property) is null) 
                        childElement.AddChild(BuildField(context, new ViewModelMemberInfo(value, property, bindNames.Contains(property.Name))));
                }

                return childElement;
            }
        }

        private static VisualElement BuildField(ViewModelFieldsUpdater updater, ViewModelMemberInfo member)
        {
            var type = member.Type;
            var value = member.Value;
            var label = NicifyVariableName(member.Name);
            
            if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(OneTimeClassEvent<>)
                    || genericTypeDefinition == typeof(OneTimeStructEvent<,>))
                {
                    return new VisualElement();
                }
            }
            
            if (typeof(int) == type) return new IntegerField(label).SetupField((int)value, updater, member);
            if (typeof(long) == type) return new LongField(label).SetupField((long)value, updater, member);
            if (typeof(float) == type) return new FloatField(label).SetupField((float)value, updater, member);
            if (typeof(double) == type) return new DoubleField(label).SetupField((double)value, updater, member);
            if (typeof(bool) == type) return new Toggle(label).SetupField((bool)value, updater, member);
            if (typeof(string) == type) return new TextField(label).SetupField(value as string, updater, member);
            if (typeof(Color) == type) return new ColorField(label).SetupField((Color)value, updater, member);
            if (typeof(Rect) == type) return new RectField(label).SetupField((Rect)value, updater, member);
            if (typeof(RectInt) == type) return new RectIntField(label).SetupField((RectInt)value, updater, member);
            if (typeof(Bounds) == type) return new BoundsField(label).SetupField((Bounds)value, updater, member);
            if (typeof(BoundsInt) == type) return new BoundsIntField(label).SetupField((BoundsInt)value, updater, member);
            if (typeof(Vector2) == type) return new Vector2Field(label).SetupField((Vector2)value, updater, member);
            if (typeof(Vector3) == type) return new Vector3Field(label).SetupField((Vector3)value, updater, member);
            if (typeof(Vector4) == type) return new Vector4Field(label).SetupField((Vector4)value, updater, member);
            if (typeof(Vector2Int) == type) return new Vector2IntField(label).SetupField((Vector2Int)value, updater, member);
            if (typeof(Vector3Int) == type) return new Vector3IntField(label).SetupField((Vector3Int)value, updater, member);
            if (typeof(Delegate).IsAssignableFrom(type)) return BuildDelegateField(value as Delegate, label);
            if (typeof(Object).IsAssignableFrom(type)) return new ObjectField(label).SetupField(value as Object, updater, member);
            if (typeof(Gradient).IsAssignableFrom(type)) return new GradientField(label).SetupField(value as Gradient, updater, member);
            if (typeof(AnimationCurve).IsAssignableFrom(type)) return new CurveField(label).SetupField(value as AnimationCurve, updater, member);
            if (typeof(Enum).IsAssignableFrom(type)) return new EnumField(label, value as Enum).SetupField(value as Enum, updater, member);
            if (typeof(IEnumerable).IsAssignableFrom(type)) return BuildEnumerableField(value as IEnumerable, label);
            
            if (value is RelayCommand)
            {
                return BuildRelayCommandField(updater, member, label);
            }
            
            return BuildCompositeValue(member, label);
        }

        private static VisualElement BuildNullField(string label) => new TextField(label)
        { 
            value = "null",
            enabledSelf = false,
        };

        private static VisualElement BuildDelegateField(Delegate value, string label)
        {
            if (value is null)
                return BuildNullField(label);
            
            return Foldout(label, label, value.GetType(), () =>
            {
                var root = new VisualElement();

                foreach (var @delegate in value.GetInvocationList())
                {
                    if (@delegate is null)
                    {
                        root.AddChild(BuildNullField(label));
                        continue;
                    }

                    var targetType = @delegate.Method.DeclaringType!;
                    var targetName = targetType.Name;

                    var box = Elements.CreateContainer(EditorColor.LighterContainer)
                        .SetMargin(top: 5);
                    
                    if (@delegate.Target is Object obj)
                    {
                        box.AddChild(new ObjectField(label)
                        {
                            value = obj,
                            enabledSelf = false,
                        });
                    }
                    else
                    {
                        var targetNamespace = targetType.Namespace;

                        box.AddChild(new TextField("Type")
                        {
                            enabledSelf = false,
                            value = $"{targetNamespace}.{targetName}",
                        });
                    }

                    box.AddChild(new TextField("Method")
                    {
                        enabledSelf = false,
                        value = @delegate.Method.Name
                    });
                    
                    root.AddChild(box)
                        .SetMargin(bottom: 5);
                }

                return root;
            });
        }

        private static VisualElement BuildEnumerableField(IEnumerable value, string label)
        {
            if (value is null)
                return BuildNullField(label);
            
            return Foldout(label, label, value.GetType(), () =>
            {
                var root = new VisualElement();

                if (IsKeyValuePairInEnumerable())
                {
                    foreach (var item in value)
                    {
                        var keyProperty = item.GetType().GetProperty("Key");
                        var valueProperty = item.GetType().GetProperty("Value");

                        var key = keyProperty!.GetValue(item);
                        var elementValue = valueProperty!.GetValue(item);

                        var box = Elements.CreateContainer(EditorColor.LighterContainer)
                            .SetMargin(top: 5);

                        box.AddChild(BuildField(new ViewModelFieldsUpdater(), new ViewModelMemberInfo(key, "Key")));
                        box.AddChild(BuildField(new ViewModelFieldsUpdater(), new ViewModelMemberInfo(elementValue, "Value")));
                        
                        root.AddChild(box);
                    }
                    
                    root.SetMargin(bottom: 5);
                }
                else
                {
                    var index = 0;
                    foreach (var item in value)
                    {
                        root.AddChild(BuildField(new ViewModelFieldsUpdater(), new ViewModelMemberInfo(item, index.ToString())));
                        index++;
                    }
                }

                return root;
            });
            
            bool IsKeyValuePairInEnumerable()
            {
                return value.GetType()
                    .GetInterfaces()
                    .Any(@interface =>
                    {
                        if (!@interface.IsGenericType) return false;
                        if (!typeof(IEnumerable<>).IsAssignableFrom(@interface.GetGenericTypeDefinition()))
                            return false;

                        var interfaceArgument = @interface.GetGenericArguments()[0];
                        return interfaceArgument.IsGenericType &&
                            typeof(KeyValuePair<,>).IsAssignableFrom(interfaceArgument.GetGenericTypeDefinition());
                    });
            }
        }
        
        private static VisualElement BuildRelayCommandField(ViewModelFieldsUpdater updater, ViewModelMemberInfo member, string label)
        {
            return Foldout(label, member.Name + label, member.Type, DataContainer);
            
            VisualElement DataContainer()
            {
                var root = new VisualElement();

                var type = typeof(RelayCommand);
                var execute = type.GetField("_execute", Flags);
                var canExecute = type.GetField("_canExecute", Flags);
                var canExecuteChanged = type.GetField("CanExecuteChanged", Flags);

                var value = member.Value;
                root.AddChild(BuildDelegateField(execute?.GetValue(value) as Delegate, "Execute"));
                root.AddChild(BuildDelegateField(canExecute?.GetValue(value) as Delegate, "CanExecute"));
                root.AddChild(BuildDelegateField(canExecuteChanged?.GetValue(value) as Delegate, "CanExecuteChanged"));
                
                var executeMethod = type.GetMethod("Execute", Flags);
                var canExecuteMethod = type.GetMethod("CanExecute", Flags);
                
                root.AddChild(updater.CreateButton("Execute", () => executeMethod?.Invoke(value, new object[] { })));
                root.AddChild(updater.CreateButton("Can Execute", () => canExecuteMethod?.Invoke(value, new object[] { })));
                
                return root;
            }
        }

        private static VisualElement SetupField<T>(this BaseField<T> field, T value, ViewModelFieldsUpdater updater, ViewModelMemberInfo memberInfo)
        {
            if (!string.IsNullOrEmpty(memberInfo.Tag))
                field.AddChild(new HelpBox(memberInfo.Tag, HelpBoxMessageType.None));
            
            field.value = value;
            field.enabledSelf = !memberInfo.IsReadonly;
            
            updater.AddField(field, memberInfo);
            return field;
        }

        private static Foldout Foldout(string name, string prefsKey, Type type, Func<VisualElement> trueCallBack)
        {
            var label = string.IsNullOrEmpty(type.Namespace)
                ? $"{NicifyVariableName(name)} ({type.Name})"
                : $"{NicifyVariableName(name)} ({type.Namespace}.{type.Name})";

            var foldout = new Foldout
            {
                text = label,
                value = EditorPrefs.GetBool(prefsKey, false),
            }
            .SetMargin(left: 15);
                
            foldout.RegisterValueChangedCallback(e =>
            {
                if (e.target == foldout)
                    SetValue(e.newValue);
            });
            
            SetValue(foldout.value);
            return foldout;

            void SetValue(bool value)
            {
                foldout.Clear();
                EditorPrefs.SetBool(prefsKey, value);
                
                if (value)
                {
                    foldout.AddChild(trueCallBack?.Invoke());
                }
            }
        }
        
        private static string NicifyVariableName(string name)
        {
            var nameWithoutPrefix = name.Remove(0, name.GetPrefixCount());
            return ObjectNames.NicifyVariableName(nameWithoutPrefix);
        }
    }
}