using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.TodoList
{
    [View]
    public sealed partial class EditTextDialogView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _text;
        
        // BindId overrides the binding id when the field name does not match the ViewModel member.
        [BindId("CancelCommand")]
        [SerializeField] private ButtonCommandBinder[] _cancelButton;
        
        [BindId("RenamedCommand")]
        [SerializeField] private ButtonCommandBinder[] _renamedButton;
    }
}