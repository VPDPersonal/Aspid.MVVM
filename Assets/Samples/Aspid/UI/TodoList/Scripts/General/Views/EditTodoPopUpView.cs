using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders.Commands;

namespace Aspid.UI.TodoList.Views
{
    [View]
    public partial class EditTodoPopUpView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder _text;
        
        [SerializeField] private ButtonCommandProvider _cancelCommand;
        [SerializeField] private ButtonCommandProvider _renamedCommand;
    }
}