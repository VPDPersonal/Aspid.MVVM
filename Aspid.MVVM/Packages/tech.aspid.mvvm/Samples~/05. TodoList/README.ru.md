# Туториал 5. TodoList

Разбор Sample-проекта TodoList — коллекции, синхронизация, фильтрация, диалоги.

---

## Что строим

Список задач с добавлением, удалением, редактированием, поиском и отметкой выполнения. Демонстрирует работу с `ObservableList`, `CreateSync`, динамическое создание View из префабов.

Файлы: `Samples~/05. TodoList/`. Сцена: `Scenes/Todo List.unity`.

**Предполагается знание:** [Stats](../04.%20Stats/README.ru.md).

---

## Зачем нужна отдельная Model?

В Counter и Greeter ViewModel **сам являлся** состоянием. Но когда логика становится сложнее (сохранение, сетевые запросы, несколько экранов), её выносят в Model.

**Model** — чистый C# класс, не знающий ни о Unity, ни о ViewModel. ViewModel лишь **адаптирует** Model для View.

```
Model          ViewModel          View
(бизнес-       (адаптер,          (UI,
логика)        биндинг)           биндеры)
```


---

## Модель

### Todo

```csharp
public sealed class Todo : IReadOnlyTodo
{
    public string Id { get; }
    public string Text { get; set; }
    public bool IsCompleted { get; set; }

    public Todo(string id) { Id = id; }
}
```

### TodoStorage

Хранилище с `ObservableList<Todo>`:

```csharp
public sealed class TodoStorage : IEnumerable<Todo>
{
    private readonly ObservableList<Todo> _todos = new();
    public IReadOnlyObservableList<Todo> Todos => _todos;

    public void Add(string text = "", bool isCompleted = false)
    {
        var newTodo = new Todo(Guid.NewGuid().ToString())
        {
            Text = text, IsCompleted = isCompleted
        };
        _todos.Add(newTodo);
    }

    public void Remove(string id) { /* ... */ }
}
```

---

## ViewModels

### TodoItemViewModel — элемент списка

```csharp
[ViewModel]
public sealed partial class TodoItemViewModel
{
    [Access(Access.Public)]
    [OneWayBind] private bool _isVisible;

    [OneTimeBind] private readonly IRelayCommand _editCommand;
    [OneTimeBind] private readonly IRelayCommand _deleteCommand;

    // Кастомные свойства с ручной привязкой
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
    public bool IsCompleted
    {
        get => Todo.IsCompleted;
        set
        {
            if (Todo.IsCompleted == value) return;
            Todo.IsCompleted = value;
            OnIsCompletedPropertyChanged();
        }
    }

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

**Паттерны:**
- Кастомные свойства `Text` и `IsCompleted` проксируют значения из `Todo` — привязка напрямую к модели
- `OnTextPropertyChanged()` / `OnIsCompletedPropertyChanged()` — ручное уведомление об изменении
- `[Access(Access.Public)]` — публичный доступ к `IsVisible` для управления из родительского ViewModel
- `[OneTimeBind] private readonly` — команды задаются в конструкторе, не меняются
- `CreateCommandWithoutParametersOrEmpty(this)` — конвертирует `IRelayCommand<T>` в `IRelayCommand`, подставляя `this`

### TodoStorageViewModel — список задач

```csharp
[ViewModel]
public sealed partial class TodoStorageViewModel : IDisposable
{
    [TwoWayBind]
    [TwoWayBind] private string _searchInput = string.Empty;

    [OneTimeBind]
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
    private void AddTodo()
    {
        _countAddedTodo++;
        _todoStorage.Add($"New Todo {_countAddedTodo}");
    }

    partial void OnSearchInputChanged(string newValue)
    {
        foreach (var viewModel in TodoItemViewModels)
            SetTodoItemVisible(viewModel);
    }

    private void SetTodoItemVisible(TodoItemViewModel viewModel) =>
        viewModel.IsVisible = string.IsNullOrWhiteSpace(SearchInput)
            || viewModel.Todo.Text.Contains(SearchInput);
}
```

**Ключевой паттерн: `CreateSync`**

```csharp
_todoItemViewModels = todoStorage.Todos.CreateSync(CreateTodoViewModel);
```

`CreateSync` автоматически синхронизирует `ObservableList<Todo>` с `ObservableList<TodoItemViewModel>`:
- При добавлении `Todo` → создаётся `TodoItemViewModel` через фабрику
- При удалении `Todo` → удаляется соответствующий `TodoItemViewModel`
- Поддерживает порядок и все операции списка

### EditTextDialogViewModel — диалог редактирования

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
        _cancelCommand = cancelled.CreateCommandOrEmpty();
        _renamedCommand = new RelayCommand(
            execute: () => renamed.Invoke(Text),
            canExecute: () => Text != text);
    }

    partial void OnTextChanged(string newValue) =>
        _renamedCommand?.NotifyCanExecuteChanged();
}
```

Кнопка «Rename» активна только если текст изменился.

---

## Views

### TodoItemView

```csharp
[View]
public sealed partial class TodoItemView : MonoView
{
    [SerializeField] private MonoBinder[] _text;
    [SerializeField] private MonoBinder[] _isCompleted;
    [SerializeField] private ButtonCommandBinder[] _editCommand;
    [SerializeField] private ButtonCommandBinder[] _deleteCommand;

    // Программный биндер — привязка видимости этого объекта
    private GameObjectVisibleBinder IsVisible => new(gameObject);
}
```

**Паттерн: программный биндер через свойство.**

`IsVisible` не `[SerializeField]` — создаётся программно. Source Generator видит его благодаря `[View]` атрибуту и включает в привязку.

### TodoStorageView

```csharp
[View]
public sealed partial class TodoStorageView : MonoView
{
    [SerializeField] private MonoBinder _searchInput;
    [SerializeField] private ButtonCommandBinder[] _addTodoCommand;
    [SerializeField] private ObservableListViewModelMonoBinder _todoItemViewModels;
}
```

`ObservableListViewModelMonoBinder` — коллекционный биндер, создающий/удаляющий View из префабов при изменении списка.

### EditTextDialogView

```csharp
[View]
public sealed partial class EditTextDialogView : MonoView
{
    [SerializeField] private MonoBinder[] _text;

    [BindId("CancelCommand")]
    [SerializeField] private ButtonCommandBinder[] _cancelButton;

    [BindId("RenamedCommand")]
    [SerializeField] private ButtonCommandBinder[] _renamedButton;
}
```

`[BindId("CancelCommand")]` — имя поля View (`_cancelButton`) не совпадает с ViewModel (`_cancelCommand`). `BindId` переопределяет имя привязки.

---

## Диалоги

```csharp
public sealed class EditTextDialog
{
    public void Open(string text, Action<string> renamed, Action cancel = null)
    {
        var view = Object.Instantiate(_prefab, _parent);

        cancel += () => Dispose();
        renamed += _ => Dispose();

        view.Initialize(new EditTextDialogViewModel(text, renamed, cancel));

        void Dispose() =>
            view.DestroyViewAndGameObject()?.DisposeViewModel();
    }
}
```

Паттерн: инстанцирование View, инициализация ViewModel с callback, уничтожение при закрытии.

---

## Ключевые паттерны

| Паттерн | Описание |
|---------|----------|
| `CreateSync` | Автосинхронизация Model → ViewModel коллекций |
| Программный биндер | `private T Property => new(...)` в View |
| `[BindId]` | Переопределение имени привязки |
| `[Access(Access.Public)]` | Публичный доступ к свойству для родительского ViewModel |
| Callback-диалоги | ViewModel с `Action` для результата |

---

## См. также

- [Коллекции](../../Documentation/ru/09-collections.md) — ObservableList, CreateSync
- [Collection Binders](../../Documentation/ru/StarterKit/collection-binders.md) — ViewModelObservableListBinder
- [View Factories](../../Documentation/ru/StarterKit/view-factories.md) — PrefabViewFactory, PrefabViewPool
- [Туториал VirtualizedList](../VirtualizedList/README.ru.md) — виртуализация больших списков

## Следующий шаг

[Custom Binder →](../06.%20CustomBinder/README.ru.md) — биндер для компонента, о котором StarterKit не знает.
