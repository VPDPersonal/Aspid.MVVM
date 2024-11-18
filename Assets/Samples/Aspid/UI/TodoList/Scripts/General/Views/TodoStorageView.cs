using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders.Commands;
using Aspid.UI.MVVM.StarterKit.Binders.Mono.Collections.Lists.ViewModels;

namespace Aspid.UI.TodoList.Views
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