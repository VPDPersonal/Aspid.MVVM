using System;
using UnityEditor;
using UnityEngine;

namespace Aspid.MVVM.Mono
{
    [CustomPropertyDrawer(typeof(BindMode))]
    [CustomPropertyDrawer(typeof(BindModeAttribute))]
    public class BindModeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var availableModes = attribute is BindModeAttribute bindModeAttribute 
                ? bindModeAttribute.Modes 
                : Array.Empty<BindMode>();

            if (availableModes.Length == 0)
            {
                availableModes = new[]
                {
                    BindMode.OneWay,
                    BindMode.TwoWay,
                    BindMode.OneTime,
                    BindMode.OneWayToSource,
                };
            }

            var currentMode = (BindMode)property.intValue;
            var selectedIndex = System.Array.IndexOf(availableModes, currentMode);

            if (selectedIndex < 0)
            {
                selectedIndex = 0;
                property.intValue = (int)availableModes[0];
            }

            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, System.Array.ConvertAll(availableModes, mode => mode.ToString()));
            property.intValue = (int)availableModes[selectedIndex];
        }
    }
}