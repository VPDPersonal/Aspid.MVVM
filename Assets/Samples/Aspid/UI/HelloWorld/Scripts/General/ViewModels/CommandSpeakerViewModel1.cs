using System;
using Aspid.UI.HelloWorld.Models;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace Aspid.UI.HelloWorld.ViewModels
{
    // TODO Aspid.UI Translate
    // Аттрибут ViewModel служит меткой для Source Generator.
    // При этом класс обязательно должен быть частичным.
    // Source Generator реализует IViewModel и анализирует другие атрибуты в классе.
    [ViewModel]
    public partial class CommandSpeakerViewModel1 : IDisposable
    {
        // TODO Aspid.UI Translate
        // Атрибут Bind служит метко для Source Generator.
        // Source Generator, на основе данного поля, создает 
        // свойство Text и событие TextChanged, благодаря которым происходит связывание.
        // Source Generator понимает следующие имена: m_text, _text, text
        [Bind] private string _text;
    
        // TODO Aspid.UI Translate
        // Поле _inputText служит только для передачи информации c View.
        // Тем не менее оно может работать в формате TwoWay.
        // OneWayToSource пока что не поддерживается.
        [Bind] private string _inputText;
    
        private readonly Speaker _speaker;
    
        public CommandSpeakerViewModel1(Speaker speaker)
        {
            _speaker = speaker;
        
            // TODO Aspid.UI Translate
            // Для инициализации поля рекомендуется использовать поле, а не его свойство
            _text = _speaker.Text;
        
            speaker.TextChanged += OnTextChanged;
        }

        // TODO Aspid.UI Translate
        // Атрибут RelayCommand служит метко для Source Generator.
        // Source Generator создает свойство только для чтения "SayCommand", которое может быть связанно
        [RelayCommand]
        private void Say() => _speaker.Say(InputText);
    
        private void OnTextChanged()
        {
            // TODO Aspid.UI Translate
            // Для работы привязки между View и ViewModel
            // необходимо обращаться к данным через их свойства, а иначе привязка не сработает.
            // Чтобы избежать такой ошибки, работает анализатор, который будет считать _text в данном контексте ошибкой.
            // Если Text == _speaker.Text, то View не получит новость об изменении так как ее нет.
            Text = _speaker.Text;
        }
    
        public void Dispose() => _speaker.TextChanged -= OnTextChanged;
    }

}