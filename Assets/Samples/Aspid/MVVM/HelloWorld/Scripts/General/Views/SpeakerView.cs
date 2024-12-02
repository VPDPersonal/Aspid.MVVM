using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Views;
using Aspid.MVVM.Views.Generation;
using Aspid.MVVM.StarterKit.Binders;

namespace Aspid.MVVM.HelloWorld.Views
{
    // The View attribute serves as a label for the Source Generator.
    // In this case, the class must necessarily be partial.
    // Source Generator implements abstract methods of MonoView and analyzes other attributes in the class.
    [View]
    public partial class SpeakerView : MonoView
    {
        // RequireBinder is not a mandatory attribute.
        // It serves only for filtering binders that implement IMonoBinderValidable
        // In this case, only those binders that implement IBinder<string> can be set in the _text field.
        [RequireBinder(typeof(string))]
        // Binder is a component that binds the visual component to data from the ViewModel.
        // Note that the field must be named exactly the same as in ViewModel.
        // But it is also worth considering that the names _text, m_text, text are equivalent.
        // The array indicates that several binders can be connected to this value.
        [SerializeField] private MonoBinder[] _text;
        
        [RequireBinder(typeof(string))]
        // Here we specify that we want to specify only one binder for binding.
        // Note that specifying a binder is optional. You will not get any log that the binder is not set.
        [SerializeField] private MonoBinder _inputText;

        // Binder does not necessarily have to inherit from MonoBinder or from MonoBehaviour. 
        // The most important condition for Source Generator is the implementation of the IBinder interface
        [SerializeField] private ButtonCommandBinder _sayCommand;
    }
}