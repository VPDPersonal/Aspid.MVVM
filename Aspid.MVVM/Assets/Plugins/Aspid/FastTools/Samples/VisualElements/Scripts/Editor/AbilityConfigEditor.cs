using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Samples.VisualElements.Editors
{
    [CustomEditor(typeof(AbilityConfig))]
    internal sealed class AbilityConfigEditor : Editor
    {
        private const string StyleSheetPath = "Styles/Aspid-FastTools-Default-Dark";
        
        public override VisualElement CreateInspectorGUI()
        {
            var config = (AbilityConfig)target;
            var root = new VisualElement()
                .AddStyleSheetsFromResource(StyleSheetPath);

            var header = new AspidInspectorHeader(config)
            {
                Subtext = "Ability configuration",
            };

            var helpBox = new AspidHelpBox(
                    title: "Zero mana cost",
                    message: "This ability costs no mana — is that intentional?", 
                    messageType: HelpBoxMessageType.Warning)
                .SetMarginTop(5);
            
            var manaConstField = new PropertyField(serializedObject.FindProperty("_manaCost"))
                .AddValueChanged(_ => UpdateState());

            var box = new AspidBox(ThemeStyle.Dark)
                .SetPadding(8)
                .SetMarginTop(5)
                .AddChild(new PropertyField(serializedObject.FindProperty("_abilityName")))
                .AddChild(new PropertyField(serializedObject.FindProperty("_description")))
                .AddChild(new PropertyField(serializedObject.FindProperty("_cooldown")))
                .AddChild(manaConstField)
                .AddChild(helpBox);

            return root
                .AddChild(header)
                .AddChild(box);
            
            void UpdateState()
            {
                var isValid = config.ManaCost is not 0;
                helpBox.SetDisplay(isValid ? DisplayStyle.None : DisplayStyle.Flex);
                header.SetStatus(isValid ? StatusStyle.Success : StatusStyle.Warning);
            }
        }
    }
}
