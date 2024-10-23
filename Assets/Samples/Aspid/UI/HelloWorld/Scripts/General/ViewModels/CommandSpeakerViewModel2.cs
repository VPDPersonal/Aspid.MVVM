using System;
using Aspid.UI.MVVM.Commands;
using Aspid.UI.HelloWorld.Models;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace Aspid.UI.HelloWorld.ViewModels
{
    // TODO Aspid.UI Translate
    // Аттрибут ViewModel служит меткой для Source Generator.
    // При этом класс обязательно должен быть частичным.
    // Source Generator реализует IViewModel и анализирует другие атрибуты в классе.
    [ViewModel]
    public partial class CommandSpeakerViewModel2 : IDisposable
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

        // TODO Aspid.UI Translate
        // Аттрибут ReadOnlyBind служит меткой для SourceGenerator
        // Source Generator генерирует так же свойство, как и при Bind, но делает доступны его только для чтения.
        // А так же не генерируется событие.
        // При этом аттрибуте возможно только OneTime связывание.
        // Такой способ связывания самый производительный.
        [ReadOnlyBind] private readonly IRelayCommand _sayCommand;
    
        private readonly Speaker _speaker;
    
        public CommandSpeakerViewModel2(Speaker speaker)
        {
            _speaker = speaker;
        
            // TODO Aspid.UI Translate
            // Для инициализации полей рекомендуется использовать поля, а не их свойство
            _text = _speaker.Text;
            _sayCommand = new RelayCommand(() => _speaker.Say(InputText));
        
            speaker.TextChanged += OnTextChanged;
        }
    
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