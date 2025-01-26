using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Generation;
using Aspid.MVVM.StarterKit.Binders;

namespace Aspid.MVVM.TodoList.Views
{
    [View]
    public partial class EditTodoPopUpView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder _text;
        
        // If the ID differs from the field name, you can redefine the ID
        [BindId("CancelCommand")]
        [SerializeField] private ButtonCommandBinder _cancelButton;
        
        [BindId("RenamedCommand")]
        [SerializeField] private ButtonCommandBinder _renamedButton;
    }
}