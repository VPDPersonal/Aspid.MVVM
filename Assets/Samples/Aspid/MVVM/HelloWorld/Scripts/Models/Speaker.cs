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
                TextChanged?.Invoke();
            }
        }
    
        public void Say(string text) => Text = text;
    }
}