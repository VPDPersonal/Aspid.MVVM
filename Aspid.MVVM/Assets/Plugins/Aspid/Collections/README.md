# Aspid.Collections

[![Unity 2021.3+](https://img.shields.io/badge/2021.3%2B-000000?style=flat&logo=unity&logoColor=white&color=4fa35d)](https://unity.com/)
[![Releases](https://img.shields.io/github/release/VPDPersonal/Aspid.Collections?color=4fa35d)](https://github.com/VPDPersonal/Aspid.Collections/releases)
[![License](https://img.shields.io/badge/License-MIT-green.svg?color=4fa35d)](LICENSE)

Observable collections library with support for covariance, collection synchronization, filtering, and sorting.

## Table of Contents

- [Installation](#installation)
- [Key Features](#key-features)
- [Collections](#collections)
  - [ObservableList](#observablelist)
  - [ObservableDictionary](#observabledictionary)
  - [ObservableHashSet](#observablehashset)
  - [ObservableQueue](#observablequeue)
  - [ObservableStack](#observablestack)
- [Interfaces](#interfaces)
- [Events](#events)
- [Collection Synchronization](#collection-synchronization)
- [Filtering and Sorting](#filtering-and-sorting)
- [Usage Examples](#usage-examples)

## Installation

Add the package to your Unity project via Package Manager:
- Package name: `com.aspid.collections`
- Minimum Unity version: 2022.3

## Key Features

- 🔔 **Observable Collections** — automatic change notifications
- 🔄 **Synchronization** — automatic synchronization between collections with type conversion
- 🔍 **Filtering** — dynamic filtering with automatic updates
- 📊 **Sorting** — dynamic sorting without modifying the source collection
- ✨ **Covariance** — support for covariant interfaces

## Collections

### ObservableList
```csharp
using Aspid.Collections.Observable;

// Creation
var list = new ObservableList<string>();
var listWithCapacity = new ObservableList<string>(10);
var listFromCollection = new ObservableList<string>(new[] { "a", "b", "c" });

// Subscribe to changes
list.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Basic operations
list.Add("item");
list.Insert(0, "first");
list[0] = "updated";
bool removed = list.Remove("item");
list.RemoveAt(0);
list.Clear();

// Clear list and events
list.Dispose();

// Batch operations
list.AddRange(new[] { "a", "b", "c" });
list.InsertRange(0, new[] { "x", "y" });

// Move
list.Move(0, 2); // Move element from index 0 to index 2
```

### ObservableDictionary
```csharp
using Aspid.Collections.Observable;

// Creation
var dict = new ObservableDictionary<string, int>();
var dictWithComparer = new ObservableDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
var dictFromCollection = new ObservableDictionary<string, int>(
    new[] { KeyValuePair.Create("a", 1), KeyValuePair.Create("b", 2) }
);

// Subscribe to changes
dict.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Operations
dict.Add("key", 42);
dict["key"] = 100;        // Replace if key exists
dict["newKey"] = 200;     // Add if key doesn't exist
bool removed = dict.Remove("key");
dict.Clear();

// Clear dictionary and events
dict.Dispose();

// Data access
bool exists = dict.TryGetValue("key", out var value);
bool contains = dict.ContainsKey("key");
```

### ObservableHashSet
```csharp
using Aspid.Collections.Observable;

// Creation
var set = new ObservableHashSet<string>();
var setWithComparer = new ObservableHashSet<string>(StringComparer.OrdinalIgnoreCase);
var setFromCollection = new ObservableHashSet<string>(new[] { "a", "b", "c" });

// Subscribe to changes
set.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Operations
bool added = set.Add("item");
bool removed = set.Remove("item");
set.Clear();

// Clear set and events
set.Dispose();
```

### ObservableQueue
```csharp
using Aspid.Collections.Observable;

// Creation
var queue = new ObservableQueue<string>();
var queueWithCapacity = new ObservableQueue<string>(10);
var queueFromCollection = new ObservableQueue<string>(new[] { "a", "b", "c" });

// Subscribe to changes
queue.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Operations
string peek = queue.Peek();
bool hasPeek = queue.TryPeek(out var peekResult);

queue.Enqueue("item");
queue.EnqueueRange(new[] { "a", "b", "c" });

string item = queue.Dequeue();
bool success = queue.TryDequeue(out var result);

queue.Clear();

// Clear queue and events
queue.Dispose();

// Batch dequeue
var buffer = new string[3];
queue.DequeueRange(buffer);
```

### ObservableStack
```csharp
using Aspid.Collections.Observable;

// Creation
var stack = new ObservableStack<string>();
var stackWithCapacity = new ObservableStack<string>(10);
var stackFromCollection = new ObservableStack<string>(new[] { "a", "b", "c" });

// Subscribe to changes
stack.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Operations
stack.Push("item");
stack.PushRange(new[] { "a", "b", "c" });

string peek = stack.Peek();
bool hasPeek = stack.TryPeek(out var peekResult);

string item = stack.Pop();
bool success = stack.TryPop(out var result);

stack.Clear();

// Clear stack and events
stack.Dispose();

// Batch pop
var buffer = new string[3];
stack.PopRange(buffer);
```

## Interfaces
### Core Interfaces

| Interface | Description |
|-----------|-------------|
| `IObservableCollection<T>` | Base interface for all observable collections |
| `IReadOnlyObservableList<T>` | Read-only list with notifications |
| `IReadOnlyObservableDictionary<TKey, TValue>` | Read-only dictionary with notifications |

### Interface Hierarchy
```
IObservableCollection<T>
├── IReadOnlyCollection<T>
├── CollectionChanged event
└── SyncRoot property

IReadOnlyObservableList<T>
├── IObservableCollection<T>
└── IReadOnlyList<T>

IReadOnlyObservableDictionary<TKey, TValue>
├── IObservableCollection<KeyValuePair<TKey, TValue>>
└── IReadOnlyDictionary<TKey, TValue>
```

## Events
### NotifyCollectionChangedEventArgs

Collection change event arguments structure:

```csharp
public readonly struct NotifyCollectionChangedEventArgs<T>
{
    // Add, Remove, Replace, Move, Reset
    public NotifyCollectionChangedAction Action { get; }
    
    // true for single-item operations
    public bool IsSingleItem { get; }                    
    
    // For single-item operations
    public T? NewItem { get; }
    public T? OldItem { get; }
    
    // For batch operations
    public IReadOnlyList<T>? NewItems { get; }
    public IReadOnlyList<T>? OldItems { get; }
    
    // Indices
    public int NewStartingIndex { get; }
    public int OldStartingIndex { get; }
}
```

### Action Types
| Action | Description |
|--------|-------------|
| `Add` | New items added |
| `Remove` | Items removed |
| `Replace` | Item replaced with another |
| `Move` | Item moved to new position |
| `Reset` | Collection cleared |

### Split Events (SplitByEvents)
For convenient handling of different change types, use the `SplitByEvents` extension:

```csharp
using Aspid.Collections.Observable;

var list = new ObservableList<string>();

// Subscribe to individual events
var events = list.SplitByEvents(
    added: (items, index) => Console.WriteLine($"Added {items.Count} items at {index}"),
    removed: (items, index) => Console.WriteLine($"Removed {items.Count} items from {index}"),
    moved: (items, oldIndex, newIndex) => Console.WriteLine($"Moved from {oldIndex} to {newIndex}"),
    replaced: (oldItems, newItems, index) => Console.WriteLine($"Replaced at {index}"),
    reset: () => Console.WriteLine("Collection cleared")
);

// Don't forget to dispose
events.Dispose();
```

## Collection Synchronization
Automatic synchronization allows creating a "mirror" collection with type conversion.

### Creating a Synchronized Collection
```csharp
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;

// Source collection of models
var models = new ObservableList<UserModel>();

// Create synchronized collection of view models
var viewModels = models.CreateSync(
    model => new UserViewModel(model),  // Converter
    isDisposable: true                  // Auto-call Dispose on removal
);

// Or with custom removal handler
var viewModels2 = models.CreateSync(
    model => new UserViewModel(model),
    removed: vm => vm.Cleanup()
);

// All changes in models are automatically reflected in viewModels
models.Add(new UserModel { Name = "John" });
// viewModels now contains UserViewModel for John

// Don't forget to dispose
viewModels.Dispose();
```

### Supported Collections for Synchronization
| Source Collection | Extension Method | Result |
|-------------------|------------------|--------|
| `IReadOnlyObservableList<T>` | `CreateSync()` | `IReadOnlyObservableListSync<T>` |
| `ObservableQueue<T>` | `CreateSync()` | `IReadOnlyObservableCollectionSync<T>` |
| `ObservableStack<T>` | `CreateSync()` | `IReadOnlyObservableCollectionSync<T>` |
| `ObservableHashSet<T>` | `CreateSync()` | `IReadOnlyObservableCollectionSync<T>` |
| `IReadOnlyObservableDictionary<K,V>` | `CreateSync()` | `IReadOnlyObservableDictionarySync<K,T>` |

## Filtering and Sorting
`FilteredList<T>` provides dynamic filtering and sorting without modifying the source collection.

### Creating a Filtered List

```csharp
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

var list = new ObservableList<int> { 5, 2, 8, 1, 9, 3 };

// Filter only
var filtered = list.CreateFiltered(x => x > 3);
// filtered contains: 5, 8, 9

// Sort only
var sorted = list.CreateFiltered(Comparer<int>.Default);
// sorted contains: 1, 2, 3, 5, 8, 9

// Filter and sort
var filteredAndSorted = list.CreateFiltered(
    filter: x => x > 2,
    comparer: Comparer<int>.Default
);
// filteredAndSorted contains: 3, 5, 8, 9
```

### Dynamic Filter Changes

```csharp
var filtered = list.CreateFiltered();

// Subscribe to changes
filtered.CollectionChanged += () =>
{
    Console.WriteLine("Filtered collection updated");
};

// Dynamic filter change
filtered.Filter = x => x > 5;

// Dynamic sort change
filtered.Comparer = Comparer<int>.Create((a, b) => b.CompareTo(a)); // Reverse order

// Force update
filtered.Update();
```

### Filter Chaining

`FilteredList` can be used as a source for another `FilteredList`:

```csharp
var list = new ObservableList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var evenNumbers = list.CreateFiltered(x => x % 2 == 0);
// evenNumbers: 2, 4, 6, 8, 10

var largeEvenNumbers = evenNumbers.CreateFiltered(x => x > 5);
// largeEvenNumbers: 6, 8, 10
```

## Usage Examples
### MVVM Pattern with Synchronization

```csharp
public class TodoListViewModel : IDisposable
{
    private readonly TodoService _service;
    private readonly IReadOnlyObservableListSync<TodoItemViewModel> _items;
    
    public IReadOnlyObservableList<TodoItemViewModel> Items => _items;
    
    public TodoListViewModel(TodoService service)
    {
        _service = service;
        
        // Automatic Model -> ViewModel synchronization
        _items = _service.Todos.CreateSync(
            model => new TodoItemViewModel(model, _service),
            isDisposable: true
        );
    }
    
    public void Dispose() => _items.Dispose();
}
```

### Observable List with Filtering
```csharp
public class SearchableListView : IDisposable
{
    private readonly ObservableList<ItemModel> _allItems;
    private readonly FilteredList<ItemModel> _visibleItems;
    
    public IReadOnlyFilteredList<ItemModel> VisibleItems => _visibleItems;
    
    public SearchableListView()
    {
        _allItems = new ObservableList<ItemModel>();
        _visibleItems = _allItems.CreateFiltered();
        
        _visibleItems.CollectionChanged += RefreshView;
    }
    
    public void SetSearchQuery(string query)
    {
        _visibleItems.Filter = string.IsNullOrEmpty(query) 
            ? null 
            : item => item.Name.Contains(query, StringComparison.OrdinalIgnoreCase);
    }
    
    public void SetSortOrder(bool ascending)
    {
        _visibleItems.Comparer = ascending
            ? Comparer<ItemModel>.Create((a, b) => string.Compare(a.Name, b.Name))
            : Comparer<ItemModel>.Create((a, b) => string.Compare(b.Name, a.Name));
    }
    
    private void RefreshView() { /* Update UI */ }
    
    public void Dispose() => _visibleItems.Dispose();
}
```

### Reactive Event Handling

```csharp
public class InventoryManager : IDisposable
{
    private readonly ObservableList<Item> _inventory = new();
    private readonly IObservableEvents<Item> _events;
    
    public InventoryManager()
    {
        _events = _inventory.SplitByEvents(
            added: (items, _) =>
            {
                foreach (var item in items)
                    Debug.Log($"Item added: {item.Name}");
            },
            removed: (items, _) =>
            {
                foreach (var item in items)
                    Debug.Log($"Item removed: {item.Name}");
            }
        );
    }
    
    public void AddItem(Item item) => _inventory.Add(item);
    public void RemoveItem(Item item) => _inventory.Remove(item);
    
    public void Dispose() => _events.Dispose();
}
```

## License

MIT License - see [LICENSE](LICENSE) file for details.

