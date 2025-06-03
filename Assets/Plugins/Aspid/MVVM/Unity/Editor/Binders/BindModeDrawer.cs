using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Aspid.MVVM.Unity
{
    [CustomPropertyDrawer(typeof(BindMode))]
    [CustomPropertyDrawer(typeof(BindModeAttribute))]
    internal sealed class BindModeDrawer : PropertyDrawer
    {
        private bool _wasLookingFor;
        private BindModeOverrideAttribute _attribute;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var availableModes = GetAvailableModes(property);

            var currentMode = (BindMode)property.intValue;
            var selectedIndex = Array.IndexOf(availableModes.Modes, currentMode);

            if (selectedIndex < 0)
            {
                selectedIndex = 0;
                property.intValue = (int)availableModes.FirstMode;
            }

            var displayedOptions = Array.ConvertAll(availableModes.Modes, mode => mode.ToString());
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, displayedOptions);
            property.intValue = (int)availableModes.Modes[selectedIndex];
        }
        
        private BindModes GetAvailableModes(SerializedProperty property)
        {
            var overrideAttribute = GetOverrideAttribute(property);
            
            if (overrideAttribute is not null)
            {
                if (overrideAttribute.IsAll || (overrideAttribute.IsOne && overrideAttribute.IsTwo))
                    return BindModes.CreateAll(overrideAttribute.Modes);
                
                if (overrideAttribute.IsOne)
                    return BindModes.CreateOne(overrideAttribute.Modes);
                
                if (overrideAttribute.IsTwo)
                    return BindModes.CreateTwo(overrideAttribute.Modes);

                return overrideAttribute.Modes.Length == 0 
                    ? BindModes.CreateAll() 
                    : BindModes.Create(overrideAttribute.Modes);
            }
            
            if (attribute is BindModeAttribute bindModeAttribute)
            {
                if (bindModeAttribute.IsAll || (bindModeAttribute.IsOne && bindModeAttribute.IsTwo))
                    return BindModes.CreateAll(bindModeAttribute.Modes);
                
                if (bindModeAttribute.IsOne)
                    return BindModes.CreateOne(bindModeAttribute.Modes);
                
                if (bindModeAttribute.IsTwo)
                    return BindModes.CreateTwo(bindModeAttribute.Modes);

                return bindModeAttribute.Modes.Length == 0 
                    ? BindModes.CreateAll() 
                    : BindModes.Create(bindModeAttribute.Modes);
            }

            return BindModes.CreateAll();
        }

        private BindModeOverrideAttribute GetOverrideAttribute(SerializedProperty property)
        {
            if (_wasLookingFor) return _attribute;
            
            var type = GetClassType(property);

            for (; type is not null; type = type.BaseType)
            {
                _attribute = type
                    .GetCustomAttributes(typeof(BindModeOverrideAttribute), false)
                    .FirstOrDefault() as BindModeOverrideAttribute;
                
                if (_attribute is not null) break;
            }
            
            _wasLookingFor = true;
            return _attribute;
        }

        private Type GetClassType(SerializedProperty property)
        {
            var path = property.propertyPath;
            var startRemoveIndex = path.Length - fieldInfo.Name.Length - 1;
            
            if (startRemoveIndex < 0)
                return property.serializedObject.targetObject.GetType();
            
            path = path.Remove(startRemoveIndex)
                .Replace(".Array.data[", "[");

            Type currentType = null;

            foreach (var part in path.Split('.'))
            {
	            currentType = part.Contains("[")
		            ? FindType(part[..part.IndexOf("[", StringComparison.Ordinal)], true)
		            : FindType(part);
            }

            return currentType;

            Type FindType(string name, bool isArray = false)
            {
	            var field = currentType is null
		            ? FindField(property.serializedObject.targetObject.GetType(), name)
		            : FindField(currentType, name);

	            if (isArray)
	            {
		            if (field.FieldType.IsArray) 
			            return field.FieldType.GetElementType();

		            if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
			            return field.FieldType.GetGenericArguments()[0];
	            }

	            return field.FieldType;
            }

            FieldInfo FindField(Type type, string name)
            {
	            return type?.GetFieldInfosIncludingBaseClasses(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
	                .FirstOrDefault(field => field.Name == name);
            }
        }

        private readonly ref struct BindModes
        {
            public readonly BindMode[] Modes;
            public readonly BindMode FirstMode;
            
            private BindModes(BindMode firstMode, params BindMode[] modes)
            {
                Modes = modes;
                FirstMode = firstMode;
            }
            
            public static BindModes Create(params BindMode[] modes) =>
                new(modes.First(), modes.Distinct().OrderBy(mode => (int)mode).ToArray());

            public static BindModes CreateOne(params BindMode[] modes)
            {
                var list = modes.ToList();
                list.Add(BindMode.OneWay);
                list.Add(BindMode.OneTime);

                return Create(list.ToArray());
            }
            
            public static BindModes CreateTwo(params BindMode[] modes)
            {
                var list = modes.ToList();
                list.Add(BindMode.TwoWay);
                list.Add(BindMode.OneWayToSource);

                return Create(list.ToArray());
            }

            public static BindModes CreateAll(params BindMode[] modes)
            {
                var list = modes.ToList();
                list.Add(BindMode.OneWay);
                list.Add(BindMode.TwoWay);
                list.Add(BindMode.OneTime);
                list.Add(BindMode.OneWayToSource);
                
                return Create(list.ToArray());
            }
        }
    }
}