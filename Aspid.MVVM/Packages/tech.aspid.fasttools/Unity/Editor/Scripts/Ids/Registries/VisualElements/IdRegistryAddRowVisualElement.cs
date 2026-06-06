using System;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal sealed class IdRegistryAddRowVisualElement : VisualElement
    {
        private readonly Button _button;
        private readonly TextField _input;
        private readonly Label _errorLabel;
        private readonly Func<string, AddRowValidation> _validate;

        public event Action<string> AddRequested;

        public IdRegistryAddRowVisualElement(Func<string, AddRowValidation> validate)
        {
            this.AddClass(Constants.Registry.Add);

            _input = new TextField()
                .AddValueChanged(_ => Revalidate());
            
            _button = new Button() 
                .SetText("+")
                .SetEnabledSelf(false)
                .AddClicked(OnAddClicked);
            
            _errorLabel = new Label()
                .SetDisplay(DisplayStyle.None);
            
            this.AddChild(new VisualElement()
                    .AddChild(_input)
                    .AddChild(_button))
                .AddChild(_errorLabel);
            
            _validate = validate;
            Revalidate();
        }
        
        public void Revalidate()
        {
            if (_validate is null) return;

            var trimmed = _input.value?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(trimmed))
            {
                _button.SetEnabled(false);
                _errorLabel.SetDisplay(DisplayStyle.None);
                
                return;
            }

            var result = _validate(trimmed);
            if (!result.IsValid)
            {
                _button.SetEnabled(false);
                _errorLabel.SetText(result.Error ?? string.Empty)
                    .SetDisplay(DisplayStyle.Flex);
                
                return;
            }

            _errorLabel.SetDisplay(DisplayStyle.None);
            _button.SetEnabled(true);
        }

        public void Reset()
        {
            _input.SetValueWithoutNotify(string.Empty);
            Revalidate();
        }

        private void OnAddClicked()
        {
            var trimmed = _input.value?.Trim();
            if (string.IsNullOrEmpty(trimmed)) return;

            AddRequested?.Invoke(trimmed);
        }
    }
}
