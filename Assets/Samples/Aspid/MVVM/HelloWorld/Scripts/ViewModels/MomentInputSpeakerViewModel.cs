using Aspid.MVVM.HelloWorld.Models;

// Explanation:
// 1. _inputText field to be bound, in our case we are not modifying the field inside ViewModel in any way,
// but expect the value to be set through View.
// 2. When _inputText is changed, we immediately pass it to the model through the OnInputTextChanged method.
namespace Aspid.MVVM.HelloWorld.ViewModels
{
    // ViewModelAttribute - marker for Source Generator.
    // For Source Generator to work properly, the class must be partial.
    // Source Generator implements IViewModel and generates related properties for labeled members.
    [ViewModel]
    public partial class MomentInputSpeakerViewModel
    {
        // OneWayToSourceBind is a token for Source Generator.
        // Source Generator, based on the labeled field
        // creates the “InputText” property for binding.
        // Source Generator works correctly with the following name style: m_inputText, _inputText, inputText.
        // For Source Generator to work, it is also necessary to label the class with ViewModelAttribute.
        [OneWayToSourceBind] private string _inputText;
    
        private readonly Speaker _speaker;
    
        public MomentInputSpeakerViewModel(Speaker speaker)
        {
            _speaker = speaker;
        }
        
        // You can implement two partial methods for each generated bound property:
        // Call before changing:
        // partial void On(Property name)Changing(string oldValue, string newValue)
        // Called after changing:
        // partial void On(Property name)Changed(string newValue)
        partial void OnInputTextChanged(string newValue)
        {
            _speaker.Say(newValue);
        }
    }
}