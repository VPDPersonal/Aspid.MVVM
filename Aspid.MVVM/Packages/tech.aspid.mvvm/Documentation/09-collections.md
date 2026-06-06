# Коллекции

Observable-коллекции с уведомлениями об изменениях, потокобезопасностью, фильтрацией и синхронизацией.

## Содержание

- [Обзор](#обзор)
- [ObservableList\<T\>](#observablelistt)
- [ObservableDictionary\<TKey, TValue\>](#observabledictionarytkey-tvalue)
- [ObservableHashSet\<T\>](#observablehashsett)
- [FilteredList\<T\>](#filteredlistt)
- [ObservableListSync](#observablelistsync)

---

## Обзор

Все коллекции из `Aspid.Collections.Observable` реализуют:

```csharp
public interface IObservableCollection<T> : IReadOnlyCollection<T>
{
    event NotifyCollectionChangedEventHandler<T>? CollectionChanged;
    object SyncRoot { get; }
}
```

**Потокобезопасность:** Все мутации защищены `lock(SyncRoot)`.

### NotifyCollectionChangedEventArgs\<T\>

`readonly struct` с информацией об изменении:

| Действие | Описание |
|----------|----------|
| `Add` | Элемент(ы) добавлены |
| `Remove` | Элемент(ы) удалены |
| `Replace` | Элемент заменён |
| `Move` | Элемент перемещён |
| `Reset` | Коллекция очищена |

Свойства: `NewItem`/`OldItem` (единичный), `NewItems`/`OldItems` (диапазон), `NewStartingIndex`/`OldStartingIndex`.

---

## ObservableList\<T\>

Потокобезопасный `IList<T>` с уведомлениями:

```csharp
var list = new ObservableList<string>();

list.CollectionChanged += (sender, args) =>
{
    switch (args.Action)
    {
        case NotifyCollectionChangedAction.Add:
            Debug.Log($"Added: {args.NewItem} at {args.NewStartingIndex}");
            break;
        case NotifyCollectionChangedAction.Remove:
            Debug.Log($"Removed: {args.OldItem}");
            break;
    }
};

list.Add("Item 1");
list.AddRange(new[] { "Item 2", "Item 3" });
list.Insert(0, "First");
list.Move(0, 2);       // Переместить элемент
list.Swap(1, 3);       // Поменять местами
list.RemoveAt(0);
list.Clear();
```

### Методы

| Метод | Описание |
|-------|----------|
| `Add(T)` | Добавить элемент |
| `AddRange(IEnumerable<T>)` | Добавить диапазон |
| `Insert(int, T)` | Вставить по индексу |
| `InsertRange(int, IEnumerable<T>)` | Вставить диапазон |
| `Remove(T)` | Удалить по значению |
| `RemoveAt(int)` | Удалить по индексу |
| `Move(int, int)` | Переместить элемент |
| `Swap(int, int)` | Поменять местами |
| `Clear()` | Очистить |

### Виртуальные хуки

Для наследников:

```csharp
protected virtual void OnAdded(T item, int index) { }
protected virtual void OnRemoved(T item, int index) { }
protected virtual void OnMoved(T item, int oldIndex, int newIndex) { }
protected virtual void OnReplaced(T oldItem, T newItem, int index) { }
protected virtual void OnClearing() { }
```

---

## ObservableDictionary\<TKey, TValue\>

Потокобезопасный словарь:

```csharp
var dict = new ObservableDictionary<string, int>();

dict.CollectionChanged += (sender, args) => { /* ... */ };

dict["health"] = 100;  // Add
dict["health"] = 80;   // Replace
dict.Remove("health"); // Remove
```

Indexer работает как "replace or add" — если ключ существует, значение заменяется.

---

## ObservableHashSet\<T\>

Потокобезопасный HashSet:

```csharp
var set = new ObservableHashSet<string>();

set.Add("tag1");     // true
set.Add("tag1");     // false (уже есть)
set.Remove("tag1");  // true
set.Clear();
```

Поддерживает set-операции: `IsSubsetOf`, `IsSupersetOf`, `Overlaps` и др.

---

## FilteredList\<T\>

Фильтрация и сортировка без изменения исходной коллекции:

```csharp
var source = new ObservableList<int> { 5, 3, 8, 1, 9, 2 };

var filtered = new FilteredList<int>(source)
{
    Filter = x => x > 3,                    // Только > 3
    Comparer = Comparer<int>.Default         // Сортировка по возрастанию
};

// filtered: [5, 8, 9]

source.Add(7);  // filtered автоматически обновляется: [5, 7, 8, 9]
source.Add(1);  // не проходит фильтр, filtered не меняется
```

### API

```csharp
public sealed class FilteredList<T> : IReadOnlyFilteredList<T>, IDisposable
{
    // Фильтр — установка вызывает Update()
    Predicate<T>? Filter { get; set; }

    // Сортировка — установка вызывает Update()
    IComparer<T>? Comparer { get; set; }

    // Количество отфильтрованных элементов
    int Count { get; }

    // Доступ по индексу (в отфильтрованном списке)
    T this[int index] { get; }

    // Принудительный пересчёт
    void Update();

    // Отписка от исходной коллекции
    void Dispose();
}
```

> **Важно:** Всегда вызывайте `Dispose()` при завершении работы, чтобы отписаться от событий исходной коллекции.

### Использование с MVVM

```csharp
[ViewModel]
public partial class ListViewModel
{
    [OneTimeBind] private ObservableList<ItemViewModel> _items;
    [OneTimeBind] private FilteredList<ItemViewModel> _filteredItems;

    public ListViewModel()
    {
        _items = new ObservableList<ItemViewModel>();
        _filteredItems = new FilteredList<ItemViewModel>(_items)
        {
            Filter = item => item.IsCompleted
        };
    }
}
```

---

## ObservableListSync

Синхронизирует две коллекции с автоматической конвертацией элементов. Основной паттерн: **Model → ViewModel**.

```csharp
// Model-коллекция
ObservableList<TodoItem> todos = storage.Todos;

// Создаём синхронизированную коллекцию ViewModel-ов
IReadOnlyObservableListSync<TodoItemViewModel> todoViewModels =
    todos.CreateSync(item => new TodoItemViewModel(item));

// todoViewModels автоматически зеркалирует все операции:
// - Add в todos → Add в todoViewModels (с конвертацией)
// - Remove в todos → Remove в todoViewModels
// - Replace, Move, Clear — аналогично
```

### Пример из TodoList sample

```csharp
[ViewModel]
public partial class TodoStorageViewModel
{
    [OneTimeBind]
    private IReadOnlyObservableListSync<TodoItemViewModel> _todoItemViewModels;

    public TodoStorageViewModel(TodoStorage todoStorage)
    {
        _todoItemViewModels = todoStorage.Todos.CreateSync(
            todo => CreateTodoViewModel(todo)
        );
    }

    private TodoItemViewModel CreateTodoViewModel(TodoItem todo)
    {
        return new TodoItemViewModel(todo, EditCommand, DeleteCommand);
    }
}
```

### С очисткой при удалении

```csharp
// Второй параметр — действие при удалении элемента
var sync = source.CreateSync(
    converter: model => new ItemViewModel(model),
    remove: vm => vm.Dispose()
);
```

---

## Привязка коллекций к View

Для отображения коллекций используйте биндеры из StarterKit:

| Биндер | Назначение |
|--------|-----------|
| `ViewModelObservableListBinder` | Динамический список с фабрикой View |
| `VirtualizedListItemSourceBinder` | Виртуализированный список |
| `ViewModelCollectionBinder<T>` | Статическая коллекция (фиксированные элементы) |

Подробнее: [Collection Binders](StarterKit/collection-binders.md).

---

## См. также

- [ViewModel](04-viewmodels.md) — привязка коллекций
- [Collection Binders](StarterKit/collection-binders.md) — биндеры для коллекций
- [VirtualizedList Tutorial](Tutorials/virtualized-list.md) — пример с виртуализацией
