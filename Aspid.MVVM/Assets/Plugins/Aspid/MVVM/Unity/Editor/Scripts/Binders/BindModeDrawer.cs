using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Custom Unity property drawer for <see cref="BindMode"/> and <see cref="BindModeAttribute"/> fields.
    /// Renders an inline dropdown populated with the modes permitted by any <see cref="BindModeOverrideAttribute"/> on the owning class.
    /// </summary>
    [CustomPropertyDrawer(typeof(BindMode))]
    [CustomPropertyDrawer(typeof(BindModeAttribute))]
    internal sealed class BindModeDrawer : PropertyDrawer
    {
        private bool _wasLookingFor;
        private object _classInstance;
        private BindModeOverrideAttribute _overrideAttribute;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InitializeOverrideAttribute(property);
            
            var availableModes = GetAvailableModes();
            var selectedIndex = GetSelectedIndex(property, availableModes);

            EditorGUI.BeginChangeCheck();
            {
                var displayedOptions = availableModes.Modes.Select(mode => mode.ToString()).ToArray();
                selectedIndex = EditorGUI.Popup(position, string.Empty, selectedIndex, displayedOptions);
            }
            if (EditorGUI.EndChangeCheck())
            {
                SetPropertyValue(property, availableModes, selectedIndex);
            }
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            InitializeOverrideAttribute(property);
            
            var availableModes = GetAvailableModes();
            var selectedIndex = GetSelectedIndex(property, availableModes);
            
            var displayedOptions = availableModes.Modes.Select(mode => mode.ToString()).ToList();
            var popup = new PopupField<string>(string.Empty, displayedOptions, selectedIndex, static data => data, static data => data)
                .SetMargin(0, 0, 0, 0);;

            popup.RegisterValueChangedCallback(_ => SetPropertyValue(property, availableModes, popup.index));
            return popup;
        }
        
        private void InitializeOverrideAttribute(SerializedProperty property)
        {
            if (_wasLookingFor) return;
            
            _classInstance = property.GetClassInstance();
            var type = _classInstance.GetType();

            for (; type is not null; type = type.BaseType)
            {
                _overrideAttribute = type
                    .GetCustomAttributes(typeof(BindModeOverrideAttribute), inherit: false)
                    .FirstOrDefault() as BindModeOverrideAttribute;
                
                if (_overrideAttribute is not null) break;
            }
            
            _wasLookingFor = true;
        }
        
        private BindModes GetAvailableModes()
        {
            if (_overrideAttribute is not null)
                return ResolveBindModes(new BindModeProviderAdapter(_overrideAttribute));

            if (attribute is BindModeAttribute bindModeAttribute)
                return ResolveBindModes(new BindModeProviderAdapter(bindModeAttribute));

            return BindModes.CreateAll();
        }

        private static BindModes ResolveBindModes(BindModeProviderAdapter provider)
        {
            if (provider.IsAll || (provider.IsOne && provider.IsTwo))
                return BindModes.CreateAll(provider.Modes);

            if (provider.IsOne)
                return BindModes.CreateOne(provider.Modes);

            if (provider.IsTwo)
                return BindModes.CreateTwo(provider.Modes);

            return provider.Modes.Length is 0
                ? BindModes.CreateAll()
                : BindModes.Create(provider.Modes);
        }

        private static int GetSelectedIndex(SerializedProperty property, BindModes availableModes)
        {
            var currentMode = (BindMode)property.intValue;
            var selectedIndex = Array.IndexOf(availableModes.Modes, currentMode);
            if (selectedIndex >= 0) return selectedIndex;
            
            selectedIndex = 0;
            property.intValue = (int)availableModes.FirstMode;
            property.serializedObject.ApplyModifiedProperties();
            return selectedIndex;
        }
        
        private void SetPropertyValue(SerializedProperty property, BindModes availableModes, int selectedIndex)
        {
            if (_classInstance is not IRebindableBinder rebindable) return;
            property.intValue = (int)availableModes.Modes[selectedIndex];
            property.serializedObject.ApplyModifiedProperties();
                
            rebindable.Rebind();
        }
        
        private readonly struct BindModes
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

        private readonly struct BindModeProviderAdapter
        {
            private readonly object _attribute;

            public BindModeProviderAdapter(BindModeAttribute attribute)
            {
                _attribute = attribute;
            }
            
            public BindModeProviderAdapter(BindModeOverrideAttribute attribute)
            {
                _attribute = attribute;
            }

            public bool IsAll => _attribute switch
            {
                BindModeAttribute a => a.IsAll,
                BindModeOverrideAttribute a => a.IsAll,
                _ => false
            };

            public bool IsOne => _attribute switch
            {
                BindModeAttribute a => a.IsOne,
                BindModeOverrideAttribute a => a.IsOne,
                _ => false
            };

            public bool IsTwo => _attribute switch
            {
                BindModeAttribute a => a.IsTwo,
                BindModeOverrideAttribute a => a.IsTwo,
                _ => false
            };

            public BindMode[] Modes => _attribute switch
            {
                BindModeAttribute a => a.Modes,
                BindModeOverrideAttribute a => a.Modes,
                _ => Array.Empty<BindMode>()
            };
        }
    }
}