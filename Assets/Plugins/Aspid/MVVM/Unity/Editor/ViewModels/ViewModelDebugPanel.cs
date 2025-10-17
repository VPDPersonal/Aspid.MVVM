using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections;
using Aspid.CustomEditors;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
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
                    if (!isBindProperty) continue;
                    
                    // TODO Add simple properties
                    container.AddChild(BuildField(new Context(property, context, true)));
                }
                
                return container;
            });
        }

        private static VisualElement BuildField(Context context)
        {
            var valueType = context.Type;
            var value = context.Value;
            var label = context.Label;
            
            VisualElement field;
            
            if (value is null) field = new NullField(label);
            else if (typeof(int) == valueType) field = new IntegerField(label).SetupField(context);
            else if (typeof(long) == valueType) field = new LongField(label).SetupField(context);
            else if (typeof(float) == valueType) field = new FloatField(label).SetupField(context);
            else if (typeof(double) == valueType) field = new DoubleField(label).SetupField(context);
            else if (typeof(bool) == valueType) field = new Toggle(label).SetupField(context);
            else if (typeof(string) == valueType) field = new TextField(label).SetupField(context);
            else if (typeof(Color) == valueType) field = new ColorField(label).SetupField(context);
            else if (typeof(Rect) == valueType) field = new RectField(label).SetupField(context);
            else if (typeof(RectInt) == valueType) field = new RectIntField(label).SetupField(context);
            else if (typeof(Bounds) == valueType) field = new BoundsField(label).SetupField(context);
            else if (typeof(BoundsInt) == valueType) field = new BoundsIntField(label).SetupField(context);
            else if (typeof(Vector2) == valueType) field = new Vector2Field(label).SetupField(context);
            else if (typeof(Vector3) == valueType) field = new Vector3Field(label).SetupField(context);
            else if (typeof(Vector4) == valueType) field = new Vector4Field(label).SetupField(context);
            else if (typeof(Vector2Int) == valueType) field = new Vector2IntField(label).SetupField(context);
            else if (typeof(Vector3Int) == valueType) field = new Vector3IntField(label).SetupField(context);
            else if (typeof(Type).IsAssignableFrom(valueType)) field = new TypeField((Type)value, label);
            else if (typeof(Object).IsAssignableFrom(valueType)) field = new ObjectField(label).SetupField(context);
            else if (typeof(Gradient).IsAssignableFrom(valueType)) field = new GradientField(label).SetupField(context);
            else if (typeof(AnimationCurve).IsAssignableFrom(valueType)) field = new CurveField(label).SetupField(context);
            else if (typeof(Delegate).IsAssignableFrom(valueType)) field = new DelegateField(value as Delegate, context.LabelWithType, context.PrefsKey);
            else if (typeof(IEnumerable).IsAssignableFrom(valueType)) field = BuildEnumerableField(context);
            else if (typeof(OneWayBindableMember).IsAssignableFrom(valueType)) field = BuildBindableMemberField(context);
            else if (typeof(TwoWayBindableMember).IsAssignableFrom(valueType)) field = BuildBindableMemberField(context);
            else if (typeof(OneWayToSourceBindableMember).IsAssignableFrom(valueType)) field = BuildBindableMemberField(context);
            else if (typeof(OneWayStructBindableMember).IsAssignableFrom(valueType)) field = BuildBindableMemberField(context);
            else if (typeof(TwoWayStructBindableMember).IsAssignableFrom(valueType)) field = BuildBindableMemberField(context);
            else if (typeof(OneWayToSourceStructBindableMember).IsAssignableFrom(valueType)) field = BuildBindableMemberField(context);
            else if (valueType.IsRelayCommandType()) field = BuildRelayCommandField(context);
            else if (typeof(Enum).IsAssignableFrom(valueType))
            {
                field = valueType.IsDefined(typeof(FlagsAttribute), false)
                    ? new EnumFlagsField(label, value as Enum).SetupField(context)
                    : new EnumField(label, value as Enum).SetupField(context);
            }
            else field = BuildCompositeValue(context);
            
            return field.SetMargin(bottom: 5);
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

                        box.AddChild(BuildField(new Context(key, "Key", context)));
                        box.AddChild(BuildField(new Context(elementValue, "Value", context)));

                        container.AddChild(box);
                    }
                }
                else
                {
                    var index = 0;
                    foreach (var item in (IEnumerable)context.Value)
                    {
                        container.AddChild(BuildField(new Context(item, index.ToString(), context)));
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
                
                var updater = context.Updaters;
                
                container.AddChild(BuildField(new Context(execute, context)));
                container.AddChild(BuildField(new Context(canExecute, context)));
                container.AddChild(BuildField(new Context(canExecuteChanged, context)));

                container.AddChild(new RelayCommandField(context.Parent.Value, value, valueType, context.Name, false,
                    () => updater.Update(),
                    () => updater.Update()));
                
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
                {
                    SetValue(e.newValue);
                    e.StopPropagation();
                }
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
            public readonly string Name;
            public readonly string Label;
            public readonly bool IsReadonly;
            public readonly string LabelWithType;
            public readonly Context Parent;
            public readonly Updaters Updaters;
            
            private readonly object _value;
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
            
            
            public string PrefsKey { get; }

            public Context(object value, string label, Context parent = null)
            {
                Name = label;
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
                        PrefsKey = LabelWithType;
                        Updaters = new Updaters();
                    }
                    else
                    {
                        Updaters = parent.Updaters;
                        PrefsKey = parent.PrefsKey + LabelWithType;
                    }
                }
            }

            public Context(FieldInfo field, Context parent) :
                this(field.GetValue(parent.Value), NicifyVariableName(field.Name), parent)
            {
                Tag = "Field";
                _field = field;
                Name = field.Name;
                IsReadonly = field.IsInitOnly;
            }

            public Context(PropertyInfo property, Context parent, bool isBind) :
                this(property.GetValue(parent.Value), NicifyVariableName(property.Name), parent)
            {
                Name = property.Name;
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
            
            public void Update()
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