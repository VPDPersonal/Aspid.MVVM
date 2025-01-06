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
        
        [SerializeField] private ButtonCommandBinder _cancelCommand;
        [SerializeField] private ButtonCommandBinder _renamedCommand;
    }
}