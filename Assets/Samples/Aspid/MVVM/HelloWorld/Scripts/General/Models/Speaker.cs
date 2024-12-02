using System;

namespace Aspid.MVVM.HelloWorld.Models
{
    public class Speaker
    {
        public event Action TextChanged;

        private string _text;
    
        public string Text
        {
            get => _text;
            private set
            {
                _text = value;
                
                // We don't need to update the View every time this event is called.
                // Since the actual text might not have changed.
                // But we may not always have access to change the model.
                // For this, ViewModel will automatically monitor the data
                // and not passing on if the value hasn't changed.
                TextChanged?.Invoke();
            }
        }
    
        public void Say(string text) => Text = text;
    }
}