using Aspid.MVVM.HelloWorld.Models;

// Explanation:
// 1. _inputText field to be bound, in our case we are not modifying the field inside ViewModel in any way,
// but expect the value to be set through View.
// 2. Say command, expects to be called from View through the linked command.
namespace Aspid.MVVM.HelloWorld.ViewModels
{
    // ViewModelAttribute - marker for Source Generator.
    // For Source Generator to work properly, the class must be partial.
    // Source Generator implements IViewModel and generates related properties for labeled members.
    [ViewModel]
    public partial class InputSpeakerViewModel
    {
        // OneWayToSourceBind is a token for Source Generator.
        // Source Generator, based on the labeled field
        // creates the “InputText” property for binding.
        // Source Generator works correctly with the following name style: m_inputText, _inputText, inputText.
        // For Source Generator to work, it is also necessary to label the class with ViewModelAttribute.
        [OneWayToSourceBind] private string _inputText;
        
        private readonly Speaker _speaker;
    
        public InputSpeakerViewModel(Speaker speaker)
        {
            _speaker = speaker;
        }
    
        // RelayCommandAttribute is a token for Source Generator.
        // Source Generator creates a read-only property “SayCommand”, based on this method.
        // The “SayCommand” property only supports OneTime and OneWay binding on the View side, since it is read-only.
        // For Source Generator to work, it is also necessary to label the class with ViewModelAttribute.
        [RelayCommand]
        private void Say()
        {
            // Although it is not necessary for reading, it is recommended to read the value through the generated property.
            // For convenience, the code analyzer works, which will generate a warning if _inputText is used.
            _speaker.Say(InputText);
        }
    }
    
    // Alternative:
    // [ViewModel]
    // public partial class InputSpeakerViewModel
    // {
    //     [OneWayToSourceBind] private string _inputText;
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