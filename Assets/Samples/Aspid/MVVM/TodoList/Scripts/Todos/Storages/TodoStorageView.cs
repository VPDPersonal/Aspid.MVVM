using UnityEngine;
using Aspid.MVVM.Unity;
using Aspid.MVVM.StarterKit.Unity;

namespace Aspid.MVVM.Samples.TodoList.Todos.Storages
{
    [View]
    public sealed partial class TodoStorageView : MonoView
    {
        [RequireBinder(typeof(string))]
        [SerializeField] private MonoBinder _searchInput;
        
        [SerializeField] private ButtonCommandBinder[] _addTodoCommand;
        [SerializeField] private ViewModelObservableListMonoBinder _todoItemViewModels;
    }
}