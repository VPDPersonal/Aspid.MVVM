# Todo List

A list of tasks with add, edit, delete, search and completion. Collections, child ViewModels and a dialog spawned at runtime.

**You learn:** a separate model, `ObservableList`, `CreateSync`, `[Bind]` on properties, `[Access]`, `[BindId]`, code-created binders, collection binders.

**Assumes:** [Stats](../04.%20Stats/README.md).

Scene: `Scenes/Todo List.unity`.

| File | Role |
|---|---|
| `Scripts/Todos/Todo.cs`, `Storages/TodoStorage.cs` | The model: a `Todo` and an `ObservableList<Todo>` storage. |
| `Scripts/Todos/TodoItemViewModel.cs`, `TodoItemView.cs` | One row of the list. |
| `Scripts/Todos/Storages/TodoStorageViewModel.cs`, `TodoStorageView.cs` | The list itself. |
| `Scripts/EditTextDialog/` | A dialog View instantiated at runtime with its own ViewModel. |
| `Scripts/Bootstraps/Bootstrap.cs` | Wires everything together. |

## Why a separate model

In Counter and Greeter the ViewModel *was* the state. Once logic grows (saving, networking, several screens) it moves into a model: plain C# that knows nothing about Unity or ViewModels. The ViewModel only adapts the model for the View.

```
Model            ViewModel          View
(business        (adapter,          (UI,
 logic)           binding)           binders)
```

```csharp
public sealed class TodoStorage : IEnumerable<Todo>
{
    private readonly ObservableList<Todo> _todos = new();

    public IReadOnlyObservableList<Todo> Todos => _todos;

    public void Add(string text = "", bool isCompleted = false) => /* ... */;
    public void Remove(Todo todo) => /* ... */;
}
```

## One row: `TodoItemViewModel`

```csharp
[ViewModel]
public sealed partial class TodoItemViewModel
{
    [Access(Access.Public)]
    [OneWayBind] private bool _isVisible;

    [OneTimeBind] private readonly IRelayCommand _editCommand;
    [OneTimeBind] private readonly IRelayCommand _deleteCommand;

    [TwoWayBind]
    public string Text
    {
        get => Todo.Text;
        set
        {
            if (Todo.Text == value) return;
            Todo.Text = value;
            OnTextPropertyChanged();
        }
    }

    [TwoWayBind]
    public bool IsCompleted { get => Todo.IsCompleted; set { /* same shape */ } }

    public readonly Todo Todo;

    public TodoItemViewModel(
        Todo todo,
        IRelayCommand<TodoItemViewModel> editCommand = null,
        IRelayCommand<TodoItemViewModel> deleteCommand = null)
    {
        Todo = todo;
        _editCommand = editCommand.CreateCommandWithoutParametersOrEmpty(this);
        _deleteCommand = deleteCommand.CreateCommandWithoutParametersOrEmpty(this);
    }
}
```

- `[TwoWayBind]` on a **property** binds straight to the model: the getter reads `Todo`, the setter writes it and calls the generated `On*PropertyChanged()` to notify binders.
- `[Access(Access.Public)]` makes the generated `IsVisible` setter public so the parent ViewModel can drive it.
- `[OneTimeBind] readonly` commands are set once in the constructor.
- `CreateCommandWithoutParametersOrEmpty(this)` turns the parent's `IRelayCommand<TodoItemViewModel>` into a parameterless `IRelayCommand` bound to this row. A `null` command becomes an empty one.

Three binding kinds meet in one View:

```csharp
[View]
public sealed partial class TodoItemView : MonoView
{
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _text;

    [RequireBinder(typeof(bool))]
    [SerializeField] private MonoBinder[] _isCompleted;

    [SerializeField] private ButtonCommandBinder[] _editCommand;
    [SerializeField] private ButtonCommandBinder[] _deleteCommand;

    private GameObjectVisibleBinder IsVisible => new(gameObject);
}
```

- `ButtonCommandBinder` is the serializable twin of `ButtonCommandMonoBinder`: a binder as a field, not a component.
- `IsVisible` is a **code-created binder**. It is not serialized, but the generator sees the property in a `[View]` class and binds it like any field.

## The list: `TodoStorageViewModel`

```csharp
[ViewModel]
public sealed partial class TodoStorageViewModel : IDisposable
{
    [TwoWayBind] private string _searchInput = string.Empty;
    [OneTimeBind] private readonly IReadOnlyObservableListSync<TodoItemViewModel> _todoItemViewModels;

    public TodoStorageViewModel(TodoStorage todoStorage, EditTextDialog editTodoDialog)
    {
        _todoStorage = todoStorage;
        _editTextDialog = editTodoDialog;
        _todoItemViewModels = todoStorage.Todos.CreateSync(CreateTodoViewModel);
    }

    private TodoItemViewModel CreateTodoViewModel(Todo todo)
    {
        var viewModel = new TodoItemViewModel(todo, OnTodoItemEditedCommand, OnTodoItemDeletedCommand);
        SetTodoItemVisible(viewModel);
        return viewModel;
    }

    [RelayCommand]
    private void AddTodo() => _todoStorage.Add($"New Todo {++_countAddedTodo}");

    [RelayCommand]
    private void OnTodoItemEdited(TodoItemViewModel viewModel) =>
        _editTextDialog.Open(viewModel.Todo.Text, text => viewModel.Text = text);

    [RelayCommand]
    private void OnTodoItemDeleted(TodoItemViewModel viewModel) =>
        _todoStorage.Remove(viewModel.Todo);

    partial void OnSearchInputChanged(string newValue)
    {
        foreach (var viewModel in TodoItemViewModels)
            SetTodoItemVisible(viewModel);
    }

    private void SetTodoItemVisible(TodoItemViewModel viewModel) =>
        viewModel.IsVisible = string.IsNullOrWhiteSpace(SearchInput) || viewModel.Todo.Text.Contains(SearchInput);

    public void Dispose() =>
        _todoItemViewModels.Dispose();
}
```

**`CreateSync`** keeps `ObservableList<Todo>` and the list of `TodoItemViewModel` in step: an added `Todo` creates a ViewModel through the factory, a removed one drops it, order and every list operation are mirrored. The sync is `IDisposable`, so the ViewModel is too.

Search does not filter the collection. It flips `IsVisible` on each row, and the row's `GameObjectVisibleBinder` hides the GameObject.

```csharp
[View]
public sealed partial class TodoStorageView : MonoView
{
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder _searchInput;

    [SerializeField] private ButtonCommandBinder[] _addTodoCommand;
    [SerializeField] private ObservableListViewModelMonoBinder _todoItemViewModels;
}
```

`ObservableListViewModelMonoBinder` instantiates a View prefab for every ViewModel in the list and destroys it when the item leaves.

## Dialog

```csharp
[ViewModel]
public sealed partial class EditTextDialogViewModel
{
    [TwoWayBind] private string _text;
    [OneTimeBind] private readonly IRelayCommand _cancelCommand;
    [OneTimeBind] private readonly IRelayCommand _renamedCommand;

    public EditTextDialogViewModel(string text, Action<string> renamed, Action cancelled)
    {
        _text = text;
        _cancelCommand = new RelayCommand(cancelled);
        _renamedCommand = new RelayCommand(
            execute: () => renamed.Invoke(Text),
            canExecute: () => Text != text);
    }

    partial void OnTextChanged(string newValue) =>
        _renamedCommand.NotifyCanExecuteChanged();
}
```

`Rename` is enabled only when the text differs from the original.

```csharp
[View]
public sealed partial class EditTextDialogView : MonoView
{
    [RequireBinder(typeof(string))]
    [SerializeField] private MonoBinder[] _text;

    [BindId("CancelCommand")]
    [SerializeField] private ButtonCommandBinder[] _cancelButton;

    [BindId("RenamedCommand")]
    [SerializeField] private ButtonCommandBinder[] _renamedButton;
}
```

`[BindId]` overrides the name-based match when the View field and the ViewModel member are named differently.

`EditTextDialog.Open` instantiates the prefab, initializes it with a fresh ViewModel and, on either result, calls `view.DestroyViewAndGameObject()?.DisposeViewModel()`.

## Summary

| Concept | Where |
|---|---|
| Model separate from ViewModel | `Todo`, `TodoStorage` |
| `CreateSync` | model list → ViewModel list, disposed with the ViewModel |
| `[Bind]` on a property | `Text`, `IsCompleted` proxy the model |
| `[Access]` | parent drives `IsVisible` |
| Code-created binder | `GameObjectVisibleBinder IsVisible => new(gameObject)` |
| `[BindId]` | field name differs from member name |
| Runtime dialog | instantiate → `Initialize` → destroy and dispose |

See also [Collections](../../Documentation/09-collections.md) and [Collection Binders](../../Documentation/StarterKit/collection-binders.md). For large lists see [Virtualized List](../VirtualizedList/README.md).

Next: [Custom Binder](../06.%20CustomBinder/README.md), a binder for a component the StarterKit does not know.

Text uses TextMeshPro (part of `com.unity.ugui`). The sample ships its own font asset in `Fonts/` (Liberation Sans, OFL), so it does not depend on the fonts from TMP Essentials.
