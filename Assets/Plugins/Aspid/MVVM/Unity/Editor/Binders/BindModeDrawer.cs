using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [CustomPropertyDrawer(typeof(BindMode))]
    [CustomPropertyDrawer(typeof(BindModeAttribute))]
    internal sealed class BindModeDrawer : PropertyDrawer
    {
        private bool _wasLookingFor;
        private (BindModeOverrideAttribute overrideAttribute, object instance) _classInfo;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InitializeOverrideAttribute(property);
            var availableModes = GetAvailableModes();

            var currentMode = (BindMode)property.intValue;
            var selectedIndex = Array.IndexOf(availableModes.Modes, currentMode);

            if (selectedIndex < 0)
            {
                selectedIndex = 0;
                property.intValue = (int)availableModes.FirstMode;
            }

            EditorGUI.BeginChangeCheck();
            {
                var displayedOptions = Array.ConvertAll(availableModes.Modes, mode => mode.ToString());
                selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, displayedOptions);
            }
            if (EditorGUI.EndChangeCheck() && _classInfo.instance is IRebindableBinder rebindable)
            {
                property.intValue = (int)availableModes.Modes[selectedIndex];
                property.serializedObject.ApplyModifiedProperties();
                
                rebindable.Rebind();
            }
        }
        
        private BindModes GetAvailableModes()
        {
            var overrideAttribute = _classInfo.overrideAttribute;
            
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

        private void InitializeOverrideAttribute(SerializedProperty property)
        {
            if (_wasLookingFor) return;
            
            var (type, classInstance) = property.GetClassInfo();
            _classInfo.instance = classInstance;

            for (; type is not null; type = type.BaseType)
            {
                _classInfo.overrideAttribute = type
                    .GetCustomAttributes(typeof(BindModeOverrideAttribute), false)
                    .FirstOrDefault() as BindModeOverrideAttribute;
                
                if (_classInfo.overrideAttribute is not null) break;
            }
            
            _wasLookingFor = true;
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