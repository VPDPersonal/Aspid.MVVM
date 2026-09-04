# Collection Binders

Binders that show ViewModel collections as lists of UI elements.

---

## Overview

| Binder | Purpose |
|--------|-----------|
| `ViewModelObservableListBinder` | Dynamic list with a View factory |
| `ViewModelCollectionBinder<T>` | Static collection (fixed Views) |
| `ViewModelObservableDictionaryBinder` | Dictionary with a View factory |
| `VirtualizedListItemSourceBinder` | Data source for a VirtualizedList |

---

## ViewModelObservableListBinder

The main binder for showing an `ObservableList<IViewModel>`. Creates and destroys Views as items are added and removed.

### How it works

```
ObservableList<IViewModel> (ViewModel)
    → ViewModelObservableListBinder
        → IViewFactory.Create(viewModel) on Add
        → IViewFactory.Release(view) on Remove
```

### Inspector properties

| Property | Type | Description |
|----------|-----|----------|
| `Factory` | `IViewFactory<MonoView>` | Factory that creates Views (PrefabViewFactory or PrefabViewPool) |
| `Filter` | `ICollectionFilter<IViewModel>` | Optional filter |
| `Order` | `ICollectionOrder<IViewModel>` | Optional ordering |

### Mode

**OneWay** and **OneTime**.

### Example ViewModel

```csharp
[ViewModel]
public partial class TodoListViewModel
{
    [OneTimeBind]
    private IReadOnlyObservableListSync<TodoItemViewModel> _items;

    public TodoListViewModel(TodoStorage storage)
    {
        _items = storage.Todos.CreateSync(
            todo => new TodoItemViewModel(todo)
        );
    }
}
```

MonoBinder variant: `ObservableListViewModelMonoBinder`.

---

## ViewModelCollectionBinder\<T\>

For static collections with pre-created Views:

```csharp
// Inspector: the _views array is filled in advance
// On bind: View[0].Initialize(collection[0]), View[1].Initialize(collection[1])...
// Extra Views are hidden through SetActive(false)
```

Fits a fixed number of items (for example 5 inventory slots).

---

## VirtualizedListItemSourceBinder

Sets the `ItemsSource` of a `VirtualizedList`:

```csharp
[ViewModel]
public partial class ListViewModel
{
    [OneTimeBind]
    private FilteredList<ItemViewModel> _filteredItems;

    [OneTimeBind]
    private ObservableList<ItemViewModel> _items;

    public ListViewModel()
    {
        _items = new ObservableList<ItemViewModel>();
        _filteredItems = new FilteredList<ItemViewModel>(_items);
    }
}
```

### Inspector properties

| Property | Description |
|----------|----------|
| `Filter` | `ICollectionFilter<IViewModel>` |
| `Order` | `ICollectionOrder<IViewModel>` |

Creates an internal `FilteredList<IViewModel>` when a filter or an order is set.

**Mode:** **OneTime**.

---

## View Factories

Factories are used by `ViewModelObservableListBinder` to create Views:

### PrefabViewFactory

Creates Views through `Object.Instantiate`:

```
Create(viewModel) → Instantiate(prefab) → SetSibling → Initialize(viewModel)
Release(view) → DestroyViewAndGameObject()
```

### PrefabViewPool

Reuses Views through an `ObjectPool`:

```
Create(viewModel) → Pool.Get() → Initialize(viewModel) → SetActive(true)
Release(view) → Deinitialize() → SetActive(false) → Pool.Release()
```

**Properties:**
- `_initialCount`: initial pool size
- `_maxCount`: maximum size

> [!TIP]
> Use `PrefabViewPool` for lists that update often (chat, feed).

More: [View Factories](view-factories.md).

---

## Filtering and ordering

### ICollectionFilter\<T\>

```csharp
public interface ICollectionFilter<in T>
{
    bool Matches(T item);
}
```

Implement it for a custom filter:

```csharp
[Serializable]
public class CompletedFilter : ICollectionFilter<IViewModel>
{
    public bool Matches(IViewModel item) =>
        item is ItemViewModel viewModel && viewModel.IsCompleted;
}
```

### ICollectionOrder\<T\>

```csharp
public interface ICollectionOrder<in T> : IComparer<T> { }
```

Implement `Compare` as for a regular `IComparer<T>`. An empty slot in the Inspector keeps the source collection order.

### Built-in filters

| Filter | Behaviour |
|--------|-----------|
| `AndCollectionFilter<T>` | Passes an item that every nested filter passes |
| `OrCollectionFilter<T>` | Passes an item that at least one nested filter passes |
| `NotCollectionFilter<T>` | Inverts the nested filter |
| `ConditionalCollectionFilter<T>` | Applies the nested filter only while `IsEnabled` is on |
| `ConverterCollectionFilter<T>` | Passes an item for which `IConverter<T, bool>` returned `true` |
| `PredicateCollectionFilter<T>` | A wrapper over `Predicate<T>` for filters from code |

An empty nested slot passes everything.

### Built-in orders

| Order | Behaviour |
|------------|-----------|
| `SequenceCollectionOrder<T>` | Applies the nested orders in turn: the first one that tells the items apart decides |
| `InverseCollectionOrder<T>` | Reverses the nested order |
| `ComparisonCollectionOrder<T>` | A wrapper over `IComparer<T>` or `Comparison<T>` for orders from code |

---

## See also

- [Collections](../09-collections.md): ObservableList, FilteredList, synchronization
- [View Factories](view-factories.md): PrefabViewFactory, PrefabViewPool
- [Virtualized List tutorial](../../Samples~/VirtualizedList/README.md), a virtualization example
