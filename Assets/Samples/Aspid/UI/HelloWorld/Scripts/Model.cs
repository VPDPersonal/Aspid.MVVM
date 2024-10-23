using System;

namespace Aspid.UI.HelloWorld
{
    public class Model
    {
        public event Action TextChanged;

        private string _text;
    
        public string Text
        {
            get => _text;
            private set
            {
                _text = value;
            
                // Для примера мы намеренно оставили вызов TextChanged,
                // даже если сам _text не изменился.
                TextChanged?.Invoke();
            }
        }
    
        public void Say(string text) => Text = text;
    }
}