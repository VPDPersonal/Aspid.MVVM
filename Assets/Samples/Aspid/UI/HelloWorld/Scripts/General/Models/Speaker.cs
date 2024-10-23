using System;

namespace Aspid.UI.HelloWorld.Models
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
                
                // TODO Aspid.UI Translate
                // Нам не нужно обновлять View каждый раз, когда это событие вызывается.
                // Так как фактически текст мог и не поменяться.
                // Но у нас не всегда может быть доступ для изменения модели.
                // Для этого ViewModel будет автоматически контролировать данные
                // и не передавая дальше, если значение не поменялось.
                TextChanged?.Invoke();
            }
        }
    
        public void Say(string text) => Text = text;
    }
}