using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.TodoList
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