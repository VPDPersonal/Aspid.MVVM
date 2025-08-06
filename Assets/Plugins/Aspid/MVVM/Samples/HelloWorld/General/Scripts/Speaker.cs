using System;

namespace Aspid.MVVM.Samples.HelloWorld
{
    public class Speaker
    {
        public event Action<string> TextChanged;

        private string _text;
    
        public string Text
        {
            get => _text;
            private set
            {
                _text = value;
                TextChanged?.Invoke(value);
            }
        }
    
        public void Say(string text) => Text = text;
    }
}