# Collections

Observable collections with change notifications, thread safety, filtering and synchronization.

## Contents

- [Overview](#overview)
- [ObservableList\<T\>](#observablelistt)
- [ObservableDictionary\<TKey, TValue\>](#observabledictionarytkey-tvalue)
- [ObservableHashSet\<T\>](#observablehashsett)
- [FilteredList\<T\>](#filteredlistt)
- [ObservableListSync](#observablelistsync)

---

## Overview

Every collection in `Aspid.Collections.Observable` implements:

```csharp
public interface IObservableCollection<T> : IReadOnlyCollection<T>
{
    event NotifyCollectionChangedEventHandler<T>? CollectionChanged;
    object SyncRoot { get; }
}
```

**Thread safety:** every mutation is guarded by `lock(SyncRoot)`.

### NotifyCollectionChangedEventArgs\<T\>

A `readonly struct` describing the change:

| Action | Description |
|----------|----------|
| `Add` | Item(s) added |
| `Remove` | Item(s) removed |
| `Replace` | Item replaced |
| `Move` | Item moved |
| `Reset` | Collection cleared |

Properties: `NewItem`/`OldItem` (single), `NewItems`/`OldItems` (range), `NewStartingIndex`/`OldStartingIndex`.

---

## ObservableList\<T\>

A thread-safe `IList<T>` with notifications:

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
list.Move(0, 2);       // Move an item
list.Swap(1, 3);       // Swap two items
list.RemoveAt(0);
list.Clear();
```

### Methods

| Method | Description |
|-------|----------|
| `Add(T)` | Add an item |
| `AddRange(IEnumerable<T>)` | Add a range |
| `Insert(int, T)` | Insert at an index |
| `InsertRange(int, IEnumerable<T>)` | Insert a range |
| `Remove(T)` | Remove by value |
| `RemoveAt(int)` | Remove by index |
| `Move(int, int)` | Move an item |
| `Swap(int, int)` | Swap two items |
| `Clear()` | Clear |

### Virtual hooks

For subclasses:

```csharp
protected virtual void OnAdded(T item, int index) { }
protected virtual void OnRemoved(T item, int index) { }
protected virtual void OnMoved(T item, int oldIndex, int newIndex) { }
protected virtual void OnReplaced(T oldItem, T newItem, int index) { }
protected virtual void OnClearing() { }
```

---

## ObservableDictionary\<TKey, TValue\>

A thread-safe dictionary:

```csharp
var dict = new ObservableDictionary<string, int>();

dict.CollectionChanged += (sender, args) => { /* ... */ };

dict["health"] = 100;  // Add
dict["health"] = 80;   // Replace
dict.Remove("health"); // Remove
```

The indexer works as "replace or add": an existing key gets its value replaced.

---

## ObservableHashSet\<T\>

A thread-safe HashSet:

```csharp
var set = new ObservableHashSet<string>();

set.Add("tag1");     // true
set.Add("tag1");     // false (already present)
set.Remove("tag1");  // true
set.Clear();
```

Supports set operations: `IsSubsetOf`, `IsSupersetOf`, `Overlaps` and others.

---

## FilteredList\<T\>

Filtering and sorting without touching the source collection:

```csharp
var source = new ObservableList<int> { 5, 3, 8, 1, 9, 2 };

var filtered = new FilteredList<int>(source)
{
    Filter = x => x > 3,                    // Only > 3
    Comparer = Comparer<int>.Default         // Ascending
};

// filtered: [5, 8, 9]

source.Add(7);  // filtered updates itself: [5, 7, 8, 9]
source.Add(1);  // fails the filter, filtered is unchanged
```

### API

```csharp
public sealed class FilteredList<T> : IReadOnlyFilteredList<T>, IDisposable
{
    // Filter; setting it calls Update()
    Predicate<T>? Filter { get; set; }

    // Sort order; setting it calls Update()
    IComparer<T>? Comparer { get; set; }

    // Number of filtered items
    int Count { get; }

    // Index access (into the filtered list)
    T this[int index] { get; }

    // Forced recalculation
    void Update();

    // Unsubscribe from the source collection
    void Dispose();
}
```

> [!IMPORTANT]
> Always call `Dispose()` when done to unsubscribe from the source collection's events.

### With MVVM

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

Keeps two collections in sync with automatic item conversion. The main pattern is **Model → ViewModel**.

```csharp
// Model collection
ObservableList<TodoItem> todos = storage.Todos;

// A synchronized collection of ViewModels
IReadOnlyObservableListSync<TodoItemViewModel> todoViewModels =
    todos.CreateSync(item => new TodoItemViewModel(item));

// todoViewModels mirrors every operation:
// - Add in todos → Add in todoViewModels (converted)
// - Remove in todos → Remove in todoViewModels
// - Replace, Move, Clear likewise
```

### Example from the Todo List sample

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

### With cleanup on removal

```csharp
// The second argument runs when an item is removed
var sync = source.CreateSync(
    converter: model => new ItemViewModel(model),
    remove: vm => vm.Dispose()
);
```

---

## Binding collections to a View

Use the StarterKit binders to display collections:

| Binder | Purpose |
|--------|-----------|
| `ViewModelObservableListBinder` | Dynamic list with a View factory |
| `VirtualizedListItemSourceBinder` | Virtualized list |
| `ViewModelCollectionBinder<T>` | Static collection (fixed items) |

More: [Collection Binders](StarterKit/collection-binders.md).

---

## See also

- [ViewModels](04-viewmodels.md), binding collections
- [Collection Binders](StarterKit/collection-binders.md), binders for collections
- [Virtualized List tutorial](../Samples~/VirtualizedList/README.md), a virtualization example
