# Туториал: TodoItem

Третий шаг обучающего маршрута. Один элемент задачи: текст, чекбокс, кнопка удаления.

**Изучаем:** отдельная Model, `IDisposable`, отписка от событий, несколько типов биндинга

**Предполагается знание:** [Greeter](greeter.md) — `[TwoWayBind]`, `On*Changed`

---

## Что строим

```
☑  Купить молоко    [×]
чекбокс  текст    кнопка удаления
```

Чекбокс синхронизируется с моделью. Удаление вызывает колбэк.

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

## Шаг 1: Model

```csharp
using System;

// Чистый C# — никакого Unity, никакого Aspid.MVVM.
public sealed class Todo
{
    public string Text { get; }
    public bool IsCompleted { get; private set; }

    public event Action<bool> CompletedChanged;

    public Todo(string text) => Text = text;

    public void Complete()
    {
        IsCompleted = true;
        CompletedChanged?.Invoke(true);
    }

    public void Restore()
    {
        IsCompleted = false;
        CompletedChanged?.Invoke(false);
    }

    public void Toggle()
    {
        if (IsCompleted) Restore();
        else Complete();
    }
}
```

Model содержит бизнес-логику и публикует события при изменении состояния.

---

## Шаг 2: ViewModel

```csharp
using System;
using Aspid.MVVM;

[ViewModel]
public sealed partial class TodoItemViewModel : IDisposable
{
    // Только чтение — текст задачи не меняется через UI.
    [OneWayBind] private string _text;

    // TwoWay — Toggle в UI записывает значение обратно в ViewModel.
    // ViewModel синхронизирует его с моделью через On*Changed.
    [TwoWayBind] private bool _isCompleted;

    private readonly Todo _todo;
    private readonly Action _onDelete;

    public TodoItemViewModel(Todo todo, Action onDelete = null)
    {
        _todo = todo;
        _onDelete = onDelete;

        _text = todo.Text;
        _isCompleted = todo.IsCompleted;

        // Подписываемся на измен ения модели — например, если Toggle вызван извне.
        _todo.CompletedChanged += SetIsCompleted;
    }

    // Когда пользователь нажимает Toggle в UI — синхронизируем с моделью.
    partial void OnIsCompletedChanged(bool newValue)
    {
        if (newValue != _todo.IsCompleted)
            _todo.Toggle();
    }

    [RelayCommand]
    private void Delete() => _onDelete?.Invoke();

    // IDisposable — отписываемся при уничтожении View.
    public void Dispose() =>
        _todo.CompletedChanged -= SetIsCompleted;
}
```

### Два направления синхронизации IsCompleted

| Направление | Как работает |
|---|---|
| Model → ViewModel → UI | `CompletedChanged` вызывает `SetIsCompleted` → биндер обновляет Toggle |
| UI → ViewModel → Model | Toggle меняет `IsCompleted` → `OnIsCompletedChanged` вызывает `_todo.Toggle()` |

Это полная двусторонняя синхронизация между Model и UI через ViewModel.

### Зачем IDisposable

ViewModel подписался на `_todo.CompletedChanged`. Если не отписаться — ссылка на ViewModel сохранится в делегате, и GC не сможет его собрать. `Dispose` вызывается через `DeinitializeView()?.DisposeViewModel()` в Bootstrap.

---

## Шаг 3: View

```csharp
using UnityEngine;
using Aspid.MVVM;

[View]
public sealed partial class TodoItemView : MonoView
{
    // Text — только отображение (OneWay).
    [SerializeField] private MonoBinder _text;

    // Toggle — двусторонний (TwoWay).
    [SerializeField] private MonoBinder _isCompleted;

    // Кнопка удаления — команда.
    [SerializeField] private MonoBinder[] _delete;
}
```

Три типа биндинга в одном View — типичная картина реальных экранов.

---

## Шаг 4: Bootstrap

```csharp
using UnityEngine;
using Aspid.MVVM;

public sealed class Bootstrap : MonoBehaviour
{
    [SerializeField] private TodoItemView _todoView;

    private void Awake()
    {
        var todo = new Todo("Купить молоко");
        var viewModel = new TodoItemViewModel(todo, onDelete: OnDelete);
        _todoView.Initialize(viewModel);
    }

    private void OnDelete()
    {
        // Здесь — удаление из списка, навигация и т.д.
        Debug.Log("Удалено");
    }

    private void OnDestroy()
    {
        // DisposeViewModel вызовет TodoItemViewModel.Dispose() — отпишет от _todo.CompletedChanged.
        _todoView.DeinitializeView()?.DisposeViewModel();
    }
}
```

---

## Шаг 5: Настройка сцены

### Иерархия объектов

```
TodoItem Scene
├── Bootstrap              (Bootstrap.cs)
└── Todo UI
    ├── TodoItemView       (TodoItemView.cs)
    ├── Completed Toggle   (Toggle + ToggleIsOnMonoBinder)
    ├── Text Label         (TextMeshPro + TextMonoBinder)
    └── Delete Button      (Button + ButtonCommandMonoBinder)
```

### В Inspector

1. **Completed Toggle** → добавьте `Toggle` и `ToggleIsOnMonoBinder`
2. **Text Label** → добавьте `TextMeshPro` и `TextMonoBinder`
3. **Delete Button** → добавьте `Button` и `ButtonCommandMonoBinder`
4. **TodoItemView** → разложите биндеры по полям: `_text`, `_isCompleted`, `_delete`

---

## Резюме

| Концепция | Что сделали |
|-----------|-------------|
| Model | `Todo` — бизнес-логика отдельно от UI |
| `IDisposable` | Отписка от `CompletedChanged` при уничтожении |
| Двусторонняя синхронизация | UI ↔ ViewModel ↔ Model через `On*Changed` + событие |
| Несколько типов биндинга | `OneWay` (текст) + `TwoWay` (чекбокс) + `RelayCommand` (удаление) |

---

## Следующий шаг

[TodoList →](todo-list.md) — список из нескольких TodoItem. `ObservableList`, `CreateSync`, коллекционные биндеры.
