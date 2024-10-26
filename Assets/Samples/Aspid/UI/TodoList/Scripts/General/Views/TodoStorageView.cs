using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Views.Generation;
using Aspid.UI.MVVM.StarterKit.Binders.Commands;
using Aspid.UI.MVVM.StarterKit.Binders.Mono.Collections.Lists.Lists;

namespace Aspid.UI.TodoList.Views
{
    [View]
    public partial class TodoStorageView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder _searchInput;
        
        [SerializeField] private ButtonCommandProvider _addTodoCommand;
        [SerializeField] private PoolMonoViewListList _todoItemViewModels;
    }
}