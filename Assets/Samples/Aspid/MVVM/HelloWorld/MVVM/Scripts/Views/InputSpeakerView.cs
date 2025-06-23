using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.Samples.HelloWorld.MVVM
{
    // ViewAttribute is a marker for Source Generator.
    // For Source Generator to work properly, the class must be partial.
    // Source Generator implements IView and generates bind and unbind binders in initialize and de-initialize methods.
    [View]
    public sealed partial class InputSpeakerView : MonoView
    {
        // RequireBinderAttribute is an optional attribute.
        // It is only needed to filter binders that implement IMonoBinderValidable.
        // In this case, only binders that implement IBinder<string> can be set to this field.
        // A binder is a component that binds a component to data from the ViewModel.
        // It is worth noting that the field must be named exactly the same as the field in the ViewModel,
        // and although it can be overridden, it is recommended to call it exactly the same.
        // The fields: m_inputText, _inputText and inputText are equivalent.
        // MonoBinder is the base class for all binders that should inherit from MonoBehaviour.
        // _inputText can only accept one binder. This approach is convenient when we know for sure that there will be one binder.
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder _inputText;
        
        // _sayCommand is declared as an array - this is handy so that we can specify countless binders.
        [RequireBinder(typeof(IRelayCommand))]
        [SerializeField] private MonoBinder[] _sayCommand;
    }
}