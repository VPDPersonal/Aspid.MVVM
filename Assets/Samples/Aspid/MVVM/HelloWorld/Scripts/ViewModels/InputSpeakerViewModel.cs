using Aspid.MVVM.HelloWorld.Models;

// Объяснение:
// 1. _inputText поле, которое будет связано, в нашем случае мы никак не изменяем поле внутри ViewModel,
// а ожидаем, что значение будет устанавливаться через View.
// 2. Команда Say, ожидает вызов из View через связанную команду.
namespace Aspid.MVVM.HelloWorld.ViewModels
{
    // ViewModelAttribute - маркер для Source Generator.
    // Для правильной работы Source Generator, класс должен быть partial.
    // Source Generator реализует IViewModel и сгенерирует связанные свойства для маркированных членов.
    [ViewModel]
    public partial class InputSpeakerViewModel
    {
        // BindAttribute - маркер для Source Generator.
        // Source Generator, на основе маркированного поля
        // создает свойство "InputText" и событие "InputTextChanged" для связывания.
        // Source Generator корректно работает со следующим стилем имен: m_inputText, _inputText, inputText.
        // Для работы Source Generator так же необходимо маркировать класс с помощью ViewModelAttribute.
        [Bind] private string _inputText;
        
        private readonly Speaker _speaker;
    
        public InputSpeakerViewModel(Speaker speaker)
        {
            _speaker = speaker;
        }
    
        // RelayCommandAttribute - маркер для Source Generator.
        // Source Generator создает read-only свойство “SayCommand”, на основе данного метода.
        // Свойство "SayCommand" поддерживает только OneTime и OneWay связывание со стороны View, так как оно read-only.
        // Для работы Source Generator так же необходимо маркировать класс с помощью ViewModelAttribute.
        [RelayCommand]
        private void Say()
        {
            // Хоть для чтения это не обязательно, рекомендуется читать значение, через сгенерированное свойство.
            // Для удобства работает анализатор кода, который выдаст предупреждение, если использовать _inputText.
            _speaker.Say(InputText);
        }
    }
    
    // Alternative:
    // [ViewModel]
    // public partial class InputSpeakerViewModel
    // {
    //     [Bind] private string _inputText;
    //     [Bind] private readonly IRelayCommand _sayCommand;
    //     
    //     private readonly Speaker _speaker;
    //
    //     public InputSpeakerViewModel(Speaker speaker)
    //     {
    //         _speaker = speaker;
    //         _sayCommand = new RelayCommand(Say);
    //     }
    //     
    //     private void Say() => 
    //         _speaker.Say(InputText);
    // }
}