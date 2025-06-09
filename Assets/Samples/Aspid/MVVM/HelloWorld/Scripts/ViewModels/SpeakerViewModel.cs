using System;
using Aspid.MVVM.HelloWorld.Models;

// Explanation:
// 1. _text field, which will be bound, in our case we will change only in ViewModel,
// and View will only deal with display.
// 2. When we change the text of the model, we immediately update _text.
namespace Aspid.MVVM.HelloWorld.ViewModels
{
    // ViewModelAttribute - marker for Source Generator.
    // For Source Generator to work properly, the class must be partial.
    // Source Generator implements IViewModel and generates related properties for labeled members.
    [ViewModel]
    public partial class SpeakerViewModel : IDisposable
    {
        // BindAttribute - marker for Source Generator.
        // Source Generator, based on the labeled field
        // creates “Text” property and “TextChanged” event for binding.
        // Source Generator works correctly with the following name style: m_text, _text, text.
        // For Source Generator to work, it is also necessary to label the class with ViewModelAttribute.
        [OneWayBind] private string _text;
        
        private readonly Speaker _speaker;

        public SpeakerViewModel(Speaker speaker)
        {
            _speaker = speaker;
            
            // It is recommended to use a field instead of a generated property to initialize the value.
            _text = speaker.Text;
            
            _speaker.TextChanged += OnTextChanged;
        }

        private void OnTextChanged()
        {
            // For binding between View and ViewModel to work, the data must be accessed through their generated properties.
            // be accessed through their generated properties, otherwise the binding will not work.
            // To avoid such an error, the code analyzer works, which will generate an error if _text is used
            Text = _speaker.Text;
        }

        public void Dispose() => 
            _speaker.TextChanged -= OnTextChanged;
    }
}