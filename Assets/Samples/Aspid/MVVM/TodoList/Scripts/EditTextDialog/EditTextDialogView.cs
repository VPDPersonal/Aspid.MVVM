using UnityEngine;
using Aspid.MVVM.Unity;
using Aspid.MVVM.StarterKit.Unity;

namespace Aspid.MVVM.TodoList.EditTodoDialogs
{
    [View]
    public sealed partial class EditTextDialogView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder[] _text;
        
        // If the ID differs from the field name, you can redefine the ID
        [BindId("CancelCommand")]
        [SerializeField] private ButtonCommandBinder[] _cancelButton;
        
        [BindId("RenamedCommand")]
        [SerializeField] private ButtonCommandBinder[] _renamedButton;
    }
}