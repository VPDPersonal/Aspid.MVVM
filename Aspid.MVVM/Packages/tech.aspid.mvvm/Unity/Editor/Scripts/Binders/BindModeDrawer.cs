#if UNITY_EDITOR && !ASPID_MVVM_EDITOR_DISABLED
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
        private object _classInstance;
        private BindModeOverrideAttribute _overrideAttribute;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InitializeOverrideAttribute(property);
            
            var availableModes = GetAvailableModes();
            var selectedIndex = GetSelectedIndex(property, availableModes);

            var wasMixed = EditorGUI.showMixedValue;
            EditorGUI.showMixedValue = property.hasMultipleDifferentValues;

            EditorGUI.BeginChangeCheck();
            {
                // The GUIContent label overload needs GUIContent options, otherwise the label is lost
                // and a nested BindMode field is drawn unnamed.
                var displayedOptions = availableModes.Modes.Select(mode => new GUIContent(mode.ToString())).ToArray();
                selectedIndex = EditorGUI.Popup(position, label, selectedIndex, displayedOptions);
            }
            var changed = EditorGUI.EndChangeCheck();

            EditorGUI.showMixedValue = wasMixed;

            if (changed) SetPropertyValue(property, availableModes, selectedIndex);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            InitializeOverrideAttribute(property);
            
            var availableModes = GetAvailableModes();
            var selectedIndex = GetSelectedIndex(property, availableModes);
            
            var label = string.IsNullOrEmpty(preferredLabel) ? null : preferredLabel;
            var displayedOptions = availableModes.Modes.Select(mode => mode.ToString()).ToList();
            var popup = new PopupField<string>(label, displayedOptions, selectedIndex, static data => data, static data => data)
                .SetMargin(0, 0, 0, 0);

            popup.showMixedValue = property.hasMultipleDifferentValues;
            popup.RegisterValueChangedCallback(_ => SetPropertyValue(property, availableModes, popup.index));

            return popup;
        }
        
        /// <summary>
        /// Resolves the owning instance and its <see cref="BindModeOverrideAttribute"/>, re-resolving whenever the
        /// drawer is handed a different instance.
        /// </summary>
        /// <remarks>
        /// Unity reuses one drawer instance for every element of a list and for every object in a multi-selection, so a
        /// cache that only remembers "already looked" hands the second binder the first one's allowed modes — and the
        /// dropdown then offers modes the class forbids. The cache is keyed on the instance instead.
        /// </remarks>
        private void InitializeOverrideAttribute(SerializedProperty property)
        {
            var instance = property.GetDeclaringInstance();
            if (instance is not null && ReferenceEquals(instance, _classInstance)) return;

            _classInstance = instance;
            _overrideAttribute = null;

            if (_classInstance is null) return;

            var type = _classInstance.GetType();

            for (; type is not null; type = type.BaseType)
            {
                _overrideAttribute = type
                    .GetCustomAttributes(typeof(BindModeOverrideAttribute), inherit: false)
                    .FirstOrDefault() as BindModeOverrideAttribute;
                
                if (_overrideAttribute is not null) break;
            }
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

        /// <summary>
        /// Returns the index of the property's current mode among the allowed ones, correcting a value the class does
        /// not allow.
        /// </summary>
        /// <remarks>
        /// The correction is deferred to the next editor tick rather than applied here. Writing serialized data from
        /// inside a drawer mutates the object while Unity is laying it out — which loses the change on a repaint,
        /// marks a scene dirty from a redraw alone, and cannot be undone. The dropdown shows the first allowed mode in
        /// the meantime, which is what the deferred write is about to store.
        /// </remarks>
        private static int GetSelectedIndex(SerializedProperty property, BindModes availableModes)
        {
            var currentMode = (BindMode)property.intValue;
            var selectedIndex = Array.IndexOf(availableModes.Modes, currentMode);
            if (selectedIndex >= 0) return selectedIndex;

            var deferred = property.serializedObject.targetObject;
            var path = property.propertyPath;
            var firstMode = (int)availableModes.FirstMode;

            EditorApplication.delayCall += Correct;
            return 0;

            void Correct()
            {
                EditorApplication.delayCall -= Correct;
                if (!deferred) return;

                using var serializedObject = new SerializedObject(deferred);
                var target = serializedObject.FindProperty(path);

                if (target is null || target.intValue == firstMode) return;

                target.intValue = firstMode;
                serializedObject.ApplyModifiedProperties();
            }
        }
        
        /// <summary>
        /// Stores the chosen mode and rebinds the owner when it can be rebound.
        /// </summary>
        /// <remarks>
        /// The write comes first and happens unconditionally. It used to sit behind the <see cref="IRebindableBinder"/>
        /// check, so choosing a mode on anything else — a serializable binder inside a View, a plain
        /// <see cref="BindModeAttribute"/> field — did nothing at all and the dropdown snapped back on the next repaint.
        /// </remarks>
        private void SetPropertyValue(SerializedProperty property, BindModes availableModes, int selectedIndex)
        {
            if (selectedIndex < 0 || selectedIndex >= availableModes.Modes.Length) return;

            property.intValue = (int)availableModes.Modes[selectedIndex];
            property.serializedObject.ApplyModifiedProperties();

            if (_classInstance is IRebindableBinder rebindable) rebindable.Rebind();
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
#endif
