using Aspid.MVVM.Generation;
using Aspid.MVVM.HelloWorld.Models;

// Объяснение:
// 1. _inputText поле, которое будет связано, в нашем случае мы никак не изменяем поле внутри ViewModel,
// а ожидаем, что значение будет устанавливаться через View.
// 2. При изменении _inputText сразу передаем в модель, через метод OnInputTextChanged.
namespace Aspid.MVVM.HelloWorld.ViewModels
{
    // ViewModelAttribute - маркер для Source Generator.
    // Для правильной работы Source Generator, класс должен быть partial.
    // Source Generator реализует IViewModel и сгенерирует связанные свойства для маркированных членов.
    [ViewModel]
    public partial class MomentInputSpeakerViewModel
    {
        // BindAttribute - маркер для Source Generator.
        // Source Generator, на основе маркированного поля
        // создает свойство "InputText" и событие "InputTextChanged" для связывания.
        // Source Generator корректно работает со следующим стилем имен: m_inputText, _inputText, inputText.
        // Для работы Source Generator так же необходимо маркировать класс с помощью ViewModelAttribute.
        [Bind] private string _inputText;
    
        private readonly Speaker _speaker;
    
        public MomentInputSpeakerViewModel(Speaker speaker)
        {
            _speaker = speaker;
        }
        
        // Вы можете реализовать два partial метода для каждого сгенерированного связанного свойства:
        // Вызов перед изменением:
        // partial void On(Property name)Changing(string oldValue, string newValue)
        // Вызов после изменения:
        // partial void On(Property name)Changed(string newValue)
        partial void OnInputTextChanged(string newValue)
        {
            _speaker.Say(newValue);
        }
    }
}