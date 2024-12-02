using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Views;
using Aspid.MVVM.Views.Generation;
using Aspid.MVVM.StarterKit.Binders;
using Aspid.MVVM.StarterKit.Binders.Mono;

namespace Aspid.MVVM.TodoList.Views
{
    [View]
    public partial class TodoStorageView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder _searchInput;
        
        [SerializeField] private ButtonCommandBinder _addTodoCommand;
        [SerializeField] private PoolViewModelMonoList _todoItemViewModels;
    }
}