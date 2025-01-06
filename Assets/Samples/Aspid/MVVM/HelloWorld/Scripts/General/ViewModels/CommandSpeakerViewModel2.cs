using System;
using Aspid.MVVM.Generation;
using Aspid.MVVM.HelloWorld.Models;

namespace Aspid.MVVM.HelloWorld.ViewModels
{
    // The ViewModel attribute serves as a label for the Source Generator.
    // In this case, the class must necessarily be partial.
    // Source Generator implements IViewModel and analyzes other attributes in the class.
    [ViewModel]
    public partial class CommandSpeakerViewModel2 : IDisposable
    {
        // The Bind attribute serves as a marker for the Source Generator.
        // Source Generator, on the basis of this field, creates the 
        // Text property and TextChanged event, thanks to which the binding takes place.
        // Source Generator understands the following names: m_text, _text, text
        [Bind] private string _text;
    
        // The _inputText field is used only for passing information from View.
        // However, it can work in TwoWay format.
        // OneWayToSource is not supported yet.
        [Bind] private string _inputText;
        
        // The ReadOnlyBind attribute serves as a label for SourceGenerator
        // Source Generator generates the same property as Bind, but makes it read-only.
        // It also does not generate an event.
        // Only OneTime binding is possible with this attribute.
        // This way of binding is the most productive.
        [ReadOnlyBind] private readonly IRelayCommand _sayCommand;
    
        private readonly Speaker _speaker;
    
        public CommandSpeakerViewModel2(Speaker speaker)
        {
            _speaker = speaker;
        
            // It is recommended to use a field rather than its property to initialize a field
            _text = _speaker.Text;
            _sayCommand = new RelayCommand(() => _speaker.Say(InputText));
        
            speaker.TextChanged += OnTextChanged;
        }
    
        private void OnTextChanged()
        {
            // For the binding to work between View and ViewModel
            // data must be accessed through their properties, otherwise the binding will not work.
            // To avoid such an error, an analyzer works, which will consider _text in this context as an error.
            // If Text == _speaker.Text, then View will not receive the news about the change because it does not exist.
            Text = _speaker.Text;
        }
    
        public void Dispose() => _speaker.TextChanged -= OnTextChanged;
    }
}