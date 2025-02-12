using System;
using Aspid.MVVM.Generation;
using Aspid.MVVM.HelloWorld.Models;

// Объяснение:
// 1. _text поле, которое будет связано, в нашем случае будем изменять только во ViewModel,
// а View будет заниматься только отображением.
// 2. При изменении текста у модели сразу же обновляем _text.
namespace Aspid.MVVM.HelloWorld.ViewModels
{
    // ViewModelAttribute - маркер для Source Generator.
    // Для правильной работы Source Generator, класс должен быть partial.
    // Source Generator реализует IViewModel и сгенерирует связанные свойства для маркированных членов.
    [ViewModel]
    public partial class SpeakerViewModel : IDisposable
    {
        // BindAttribute - маркер для Source Generator.
        // Source Generator, на основе маркированного поля
        // создает свойство "Text" и событие "TextChanged" для связывания.
        // Source Generator корректно работает со следующим стилем имен: m_text, _text, text.
        // Для работы Source Generator так же необходимо маркировать класс с помощью ViewModelAttribute.
        [Bind] private string _text;
        
        private readonly Speaker _speaker;

        public SpeakerViewModel(Speaker speaker)
        {
            _speaker = speaker;
            
            // Рекомендуется использовать свойство вместо сгенерированного поля для инициализации значения.
            _text = speaker.Text;
            
            _speaker.TextChanged += OnTextChanged;
        }

        private void OnTextChanged()
        {
            // Для работы связывания между View и ViewModel, доступ к данным должен
            // осуществляться через их сгенерированные свойства, иначе привязка не будет работать.
            // Чтобы избежать подобной ошибки, работает анализатор кода, который выдаст ошибку, если использовать _text.
            Text = _speaker.Text;
        }

        public void Dispose() => 
            _speaker.TextChanged -= OnTextChanged;
    }
}