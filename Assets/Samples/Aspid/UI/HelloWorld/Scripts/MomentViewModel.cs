using System;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace Aspid.UI.HelloWorld
{
    [ViewModel]
    public partial class MomentViewModel : IDisposable
    {
        // Атрибут Bind указывает Source Generator, что нужно
        // создать свойство Text, которое может быть связанно
        [Bind] private string _text;
    
        // Поле _inputText нужно только для View
        // Это поле отвечает за то, что будет передано в метод Say.
        [Bind] private string _inputText;
    
        private readonly Model _model;
    
        public MomentViewModel(Model model)
        {
            _model = model;
        
            // Для иницилизации полей рекомендуется
            // использовать непосредственно поле, а его свойство
            _text = _model.Text;
        
            model.TextChanged += OnTextChanged;
        }
        
        private void OnTextChanged()
        {
            // При использовании поля _text, вместо свойства,
            // привязка не будет работать.
            // Чтобы избежать такой ошибки, работает анализатор,
            // который будет считать _text в данном контексте
            Text = _model.Text;
        }
        
        public void Dispose()
        {
            _model.TextChanged -= OnTextChanged;
        }
    }
}