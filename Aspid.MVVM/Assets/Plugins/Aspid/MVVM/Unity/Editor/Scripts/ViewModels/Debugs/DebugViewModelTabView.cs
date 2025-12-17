using UnityEditor;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugViewModelTabView : VisualElement
    {
        private const string SelectedClass = "debug-tab-button-selected";
        
        private readonly Button _bindButton;
        private readonly Button _otherButton;
        private readonly Button _commandButton;
        
        private readonly string _playerPrefKey;
        
        public bool IsBindSelected => _bindButton.ClassListContains(SelectedClass);
        
        public bool IsOtherSelected => _otherButton.ClassListContains(SelectedClass);
        
        public bool IsCommandsSelected => _commandButton.ClassListContains(SelectedClass);
        
        public DebugViewModelTabView(
            string playerPrefKey,
            VisualElement bindFieldsContainer,
            VisualElement otherFieldsContainer,
            VisualElement commandFieldsContainer)
        {
            _playerPrefKey = playerPrefKey;
            
            _bindButton = new Button(clickEvent: () =>
            {
                var isSelected = EditorPrefs.GetBool(playerPrefKey + "Bind", true);
                EditorPrefs.SetBool(playerPrefKey + "Bind", !isSelected);
                
                UpdateBindButton(bindFieldsContainer);
            }).SetText("Bind");
            
            _otherButton = new Button(clickEvent: () =>
            {
                var isSelected = EditorPrefs.GetBool(playerPrefKey + "Other", true);
                EditorPrefs.SetBool(playerPrefKey + "Other", !isSelected);
                
                UpdateOtherButton(otherFieldsContainer);
            }).SetText("Other");

            _commandButton = new Button(clickEvent: () =>
            {
                var isSelected = EditorPrefs.GetBool(playerPrefKey + "Command", true);
                EditorPrefs.SetBool(playerPrefKey + "Command", !isSelected);
              
                UpdateCommandButton(commandFieldsContainer);
            }).SetText("Commands");
            
            UpdateBindButton(bindFieldsContainer);
            UpdateOtherButton(otherFieldsContainer);
            UpdateCommandButton(commandFieldsContainer);
            
            this.AddChild(_bindButton)
                .AddChild(_commandButton)
                .AddChild(_otherButton);
        }

        private void UpdateBindButton(VisualElement container) =>
            UpdateVisual("Bind", _bindButton, container);
        
        private void UpdateOtherButton(VisualElement container) =>
            UpdateVisual("Other", _otherButton, container);
        
        private void UpdateCommandButton(VisualElement container) =>
            UpdateVisual("Command", _commandButton, container);

        private void UpdateVisual(string playerPrefKeyPostfix, Button button, VisualElement container)
        {
            var isSelected = EditorPrefs.GetBool(_playerPrefKey + playerPrefKeyPostfix, true);
            container.SetDisplay(isSelected ? DisplayStyle.Flex : DisplayStyle.None);
            
            button.RemoveFromClassList(SelectedClass);
            if (isSelected) button.AddToClassList(SelectedClass);
        }
    }
}
