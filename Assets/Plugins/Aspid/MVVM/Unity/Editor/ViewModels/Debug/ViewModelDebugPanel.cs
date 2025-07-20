using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections;
using Aspid.CustomEditors;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.Unity
{
    internal static class ViewModelDebugPanel
    {
        private const BindingFlags BindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        
        public static VisualElement Build(IView target)
        {
            var targetType = target.GetType();
            var property = targetType.GetProperty("ViewModel", BindingAttr);
            var value = property!.GetValue(target);
            
            return BuildCompositeValue(new Context(value, null));
        }

        private static VisualElement BuildCompositeValue(Context context)
        {
            if (context.Value is null) return new NullField(context.Label);
            
            return Foldout(context, () =>
            {
                var container = new VisualElement();

                var baseType = context.Value switch
                {
                    ScriptableObject => typeof(ScriptableObject),
                    MonoBehaviour => typeof(MonoBehaviour),
                    _ => typeof(object)
                };
                
                var members = context.Type.GetMembersInfosIncludingBaseClasses(BindingAttr, baseType);
                var fields = members.OfType<FieldInfo>();
                var properties = members.OfType<PropertyInfo>();
                
                var bindNames = new HashSet<string>(members
                    .OfType<MethodInfo>()
                    .Where(method => method.IsDefined(typeof(RelayCommandAttribute)))
                    .Select(method => method.Name + "Command"));
                
                foreach (var field in fields)
                {
                    if (field.GetCustomAttribute<BaseBindAttribute>() is not null)
                    {
                        bindNames.Add(field.GetGeneratedPropertyName());
                        continue;
                    }

                    if (field.IsDefined(typeof(GeneratedCodeAttribute)))
                    {
                        if (field.FieldType.IsInterface) continue;
                        if (field.FieldType.GetInterfaces().All(i => i != typeof(IBinderAdder))) continue;
                        
                        if (typeof(OneTimeBindableMember).IsAssignableFrom(field.FieldType)) continue;
                        if (typeof(OneTimeStructBindableMember).IsAssignableFrom(field.FieldType)) continue;
                    }
                    
                    container.AddChild(BuildField(new Context(field, context)));
                }

                foreach (var property in properties)
                {
                    if (property.GetIndexParameters().Length > 0) continue;
                    if (context.Type.GetExplicitInterface(property) is not null) continue;

                    var isBindProperty = bindNames.Contains(property.Name);
                    if (!isBindProperty && property.IsDefined(typeof(GeneratedCodeAttribute))) continue;
                    
                    container.AddChild(BuildField(new Context(property, context, isBindProperty)));
                }
                
                return container;
            });
        }

        private static VisualElement BuildField(Context context)
        {
            var valueType = context.Type;
            var value = context.Value;
            var label = context.Label;
            if (value is null) return new NullField(label);
            
            if (typeof(int) == valueType) return new IntegerField(label).SetupField(context);
            if (typeof(long) == valueType) return new LongField(label).SetupField(context);
            if (typeof(float) == valueType) return new FloatField(label).SetupField(context);
            if (typeof(double) == valueType) return new DoubleField(label).SetupField(context);
            if (typeof(bool) == valueType) return new Toggle(label).SetupField(context);
            if (typeof(string) == valueType) return new TextField(label).SetupField(context);
            if (typeof(Color) == valueType) return new ColorField(label).SetupField(context);
            if (typeof(Rect) == valueType) return new RectField(label).SetupField(context);
            if (typeof(RectInt) == valueType) return new RectIntField(label).SetupField(context);
            if (typeof(Bounds) == valueType) return new BoundsField(label).SetupField(context);
            if (typeof(BoundsInt) == valueType) return new BoundsIntField(label).SetupField(context);
            if (typeof(Vector2) == valueType) return new Vector2Field(label).SetupField(context);
            if (typeof(Vector3) == valueType) return new Vector3Field(label).SetupField(context);
            if (typeof(Vector4) == valueType) return new Vector4Field(label).SetupField(context);
            if (typeof(Vector2Int) == valueType) return new Vector2IntField(label).SetupField(context);
            if (typeof(Vector3Int) == valueType) return new Vector3IntField(label).SetupField(context);
            if (typeof(Type).IsAssignableFrom(valueType)) return new TypeField((Type)value, label);
            if (typeof(Object).IsAssignableFrom(valueType)) return new ObjectField(label).SetupField(context);
            if (typeof(Gradient).IsAssignableFrom(valueType)) return new GradientField(label).SetupField(context);
            if (typeof(AnimationCurve).IsAssignableFrom(valueType)) return new CurveField(label).SetupField(context);
            if (typeof(Delegate).IsAssignableFrom(valueType)) return new DelegateField(value as Delegate, context.LabelWithType, context.PrefsKey);
            if (typeof(IEnumerable).IsAssignableFrom(valueType)) return BuildEnumerableField(context);
            if (typeof(RelayCommand).IsAssignableFrom(valueType)) return BuildRelayCommandField(context);
            if (typeof(OneWayBindableMember).IsAssignableFrom(valueType)) return BuildBindableMemberField(context);
            if (typeof(TwoWayBindableMember).IsAssignableFrom(valueType)) return BuildBindableMemberField(context);
            if (typeof(OneWayToSourceBindableMember).IsAssignableFrom(valueType)) return BuildBindableMemberField(context);
            if (typeof(OneWayStructBindableMember).IsAssignableFrom(valueType)) return BuildBindableMemberField(context);
            if (typeof(TwoWayStructBindableMember).IsAssignableFrom(valueType)) return BuildBindableMemberField(context);
            if (typeof(OneWayToSourceBindableMember).IsAssignableFrom(valueType)) return BuildBindableMemberField(context);
            if (typeof(Enum).IsAssignableFrom(valueType))
            {
                return valueType.IsDefined(typeof(FlagsAttribute), false)
                    ? new EnumFlagsField(label, value as Enum).SetupField(context)
                    : new EnumField(label, value as Enum).SetupField(context);
            }

            return BuildCompositeValue(context);
        }

        private static VisualElement BuildEnumerableField(Context context)
        {
            return Foldout(context, () =>
            {
                var container = new VisualElement();

                if (IsKeyValuePairInEnumerable())
                {
                    foreach (var item in (IEnumerable)context.Value)
                    {
                        var itemType = item.GetType();
                        var keyProperty = itemType.GetProperty("Key");
                        var valueProperty = itemType.GetProperty("Value");

                        var key = keyProperty!.GetValue(item);
                        var elementValue = valueProperty!.GetValue(item);
                        
                        var box = Elements.CreateContainer(EditorColor.LighterContainer);

                        box.AddChild(BuildField(new Context(key, "Key")));
                        box.AddChild(BuildField(new Context(elementValue, "Value")));

                        container.AddChild(box);
                    }
                }
                else
                {
                    var index = 0;
                    foreach (var item in (IEnumerable)context.Value)
                    {
                        container.AddChild(BuildField(new Context(item, index.ToString())));
                        index++;
                    }
                }

                return container;
            });

            bool IsKeyValuePairInEnumerable()
            {
                return context.Type
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

        private static VisualElement BuildRelayCommandField(Context context)
        {
            return Foldout(context, () =>
            {
                var container = new VisualElement();

                var valueType = context.Type;
                var value = context.Value;
                var execute = valueType.GetField("_execute", BindingAttr);
                var canExecute = valueType.GetField("_canExecute", BindingAttr);
                var canExecuteChanged = valueType.GetField("CanExecuteChanged", BindingAttr);
                
                var executeMethod = valueType.GetMethod("Execute", BindingAttr);
                var notifyCanExecuteChangedMethod = valueType.GetMethod("NotifyCanExecuteChanged", BindingAttr);

                var updater = context.Updaters;
                
                container.AddChild(BuildField(new Context(execute, context)));
                container.AddChild(BuildField(new Context(canExecute, context)));
                container.AddChild(BuildField(new Context(canExecuteChanged, context)));
                container.AddChild(updater.CreateButton("Execute", () => executeMethod?.Invoke(value, new object[] { })));
                container.AddChild(updater.CreateButton("Notify Can Execute Changed", () => notifyCanExecuteChangedMethod?.Invoke(value, new object[] { })));
                
                return container;
            });
        }

        private static VisualElement BuildBindableMemberField(Context context)
        {
            return Foldout(context, () =>
            {
                var container = new VisualElement();

                var changed = context.Type.GetFieldInfosIncludingBaseClasses(BindingAttr)
                    .FirstOrDefault(field => field.Name == "Changed");
                container.AddChild(BuildField(new Context(changed, context)));
                
                return container;
            });
        }
        
        private static VisualElement SetupField<T>(this BaseField<T> field, Context context)
        {
            if (!string.IsNullOrEmpty(context.Tag))
                field.AddChild(new HelpBox(context.Tag, HelpBoxMessageType.None));

            field.SetValue((T)context.Value);
            field.SetEnabled(!context.IsReadonly);
            context.Updaters.AddField(field, context);

            return field;
        }

        private static VisualElement Foldout(Context context, Func<VisualElement> dataCallback)
        {
            var foldout = new Foldout()
                .SetMargin(left: 15)
                .SetText(context.LabelWithType)
                .SetValue(EditorPrefs.GetBool(context.PrefsKey, false));
            
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
                EditorPrefs.SetBool(context.PrefsKey, value);
                
                if (value)
                    foldout.AddChild(dataCallback?.Invoke());
            }
        }
        
        private class Context
        {
            public readonly Type Type;
            public readonly string Tag;
            public readonly string Label;
            public readonly bool IsReadonly;
            public readonly string LabelWithType;
            public readonly Context Parent;
            public readonly Updaters Updaters;

            private int _increment;
            private readonly object _value;
            private readonly string _prefsKey;
            private readonly FieldInfo _field;
            private readonly PropertyInfo _property;

            public object Value
            {
                get
                {
                    if (_field is not null) return _field.GetValue(Parent.Value);
                    if (_property is not null) return _property.GetValue(Parent.Value);
                    return _value;
                }
                set
                {
                    if (_field is not null) _field.SetValue(Parent.Value, value);
                    else if (_property is not null) _property.SetValue(Parent.Value, value);
                    else throw new NotImplementedException();
                }
            }

            private int Increment
            {
                get => Parent?.Increment ?? _increment;
                set
                {
                    if (Parent is not null) Parent.Increment = value;
                    else _increment = value;
                }
            }
            
            public string PrefsKey => $"{_prefsKey} {_increment}";

            public Context(object value, string label, Context parent = null)
            {
                Label = label;
                _value = value;
                Parent = parent;
                IsReadonly = true;
                Type = Value?.GetType();

                if (Type is not null)
                {
                    var typeName = Type.Namespace;
                    if (!string.IsNullOrEmpty(typeName)) typeName += ".";
                    typeName = (typeName ?? "") + Type.Name;

                    LabelWithType = string.IsNullOrEmpty(label) 
                        ? typeName 
                        : $"{label} ({typeName})";

                    if (parent is null)
                    {
                        _increment = 0;
                        _prefsKey = typeName;
                        Updaters = new Updaters();
                    }
                    else
                    {
                        Increment++;
                        _increment = Increment;
                        _prefsKey = parent._prefsKey;
                        Updaters = parent.Updaters;
                    }
                }
            }

            public Context(FieldInfo field, Context parent) :
                this(field.GetValue(parent.Value), NicifyVariableName(field.Name), parent)
            {
                Tag = "Field";
                _field = field;
                IsReadonly = field.IsInitOnly;
            }

            public Context(PropertyInfo property, Context parent, bool isBind) :
                this(property.GetValue(parent.Value), NicifyVariableName(property.Name), parent)
            {
                _property = property;
                IsReadonly = !property.CanWrite;
                Tag = isBind ? "Bind" : "Property";
            }
            
            private static string NicifyVariableName(string name)
            {
                var nameWithoutPrefix = name.Remove(0, name.GetPrefixCount());
                return ObjectNames.NicifyVariableName(nameWithoutPrefix);
            }
        }

        private class Updaters
        {
            private readonly List<Updater> _updaters = new();
            
            public void AddField<T>(BaseField<T> field, Context context)
            {
                _updaters.Add(new Updater<T>(context, field));
            
                field.RegisterValueChangedCallback(e =>
                {
                    e.StopPropagation();
                    context.Value = e.newValue;
                    Update();
                });
            }
            
            public Button CreateButton(string text, Action action) => new(() =>
            {
                action?.Invoke();
                Update();
            })
            {
                text = text,
            };
            
            private void Update()
            {
                foreach (var updater in _updaters)
                    updater.Update();
            }

            private abstract class Updater
            {
                protected readonly Context Context;

                protected Updater(Context context)
                {
                    Context = context;
                }

                public abstract void Update();
            }
            
            private class Updater<T> : Updater
            {
                private readonly BaseField<T> _field;

                public Updater(Context context, BaseField<T> field) 
                    : base(context)
                {
                    _field = field;
                }

                public override void Update() =>
                    _field.SetValueWithoutNotify((T)Context.Value);
            }
        }
    }
}